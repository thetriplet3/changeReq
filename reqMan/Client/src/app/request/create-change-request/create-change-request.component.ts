import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { RequestType } from '../../models/requestType.model';
import { Request, Attachment } from '../../models/request.model'
import { NgForm } from '@angular/forms';

import { ErrorHandler } from '../../validators/error-handler/error-handler.component'
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

declare var $: any;


@Component({
  selector: 'app-create-change-request',
  templateUrl: './create-change-request.component.html',
  styleUrls: ['./create-change-request.component.scss'],
  exportAs: 'app-create-change-request',
})
export class CreateChangeRequestComponent implements OnInit {

  requestTypes: RequestType[] = [];
  requestTypeFormPath: string;
  request: Request;
  currentUser: any;
  attachments: Attachment[];
  selectedFiles: File[];
  requestTypeTitle: string;
  requestId: string;

  isValidated: boolean = false;
  isRunning: boolean = false;
  isAfter10th: boolean = false;
  isSaveAction: boolean = false;
  isSavedRequest: boolean = false;

  STR_ATTACH_FORM: string = "Attach Form"
  STR_UPLOAD_FORM: string = "Upload Form"
  STR_DOWNLOAD_FORM: string = "Download Form";
  STR_STATUS_REQUESTED: string = "requested";
  STR_STATUS_SAVED: string = "saved";

  constructor(private requestService: RequestService, private route: ActivatedRoute, private router: Router) { }

  @ViewChild('btnAttachForm') btnAttachForm: ElementRef;

  ngOnInit() {
    this.request = new Request();
    this.request.requestType = new RequestType();
    this.request.requestType.requestTypeId = "REQ-T-002";
    this.request.dateRequested = new Date().toISOString().substring(0, 10);
    this.requestService.getRequestTypes().subscribe((data: any) => {
      this.requestTypes = data;
    });
    this.request.optOut = false;
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));

    this.requestService.preValidateRequest().subscribe((res) => {
      this.isAfter10th = res.isAfter10th;
    })

    this.route.paramMap.subscribe((paramMap: ParamMap) => {
      if (paramMap.has('requestTypeId')) {
        this.isSavedRequest = false;
        var requestTypeId = paramMap.get('requestTypeId');
        if (requestTypeId === "REQ-T-002") {
          this.STR_DOWNLOAD_FORM = "FAQ";
        }
        else {
          this.STR_DOWNLOAD_FORM = "Download Form";
        }
        this.request.requestTypeId = requestTypeId;
        this.requestService.getRequestType(requestTypeId).subscribe((data) => {
          this.requestTypeFormPath = data.formPath;
          this.requestTypeTitle = data.name;
        });
      }
      else if (paramMap.has("requestId")) {
        this.isSavedRequest = true;

        this.requestId = paramMap.get("requestId");
        this.requestService.getRequest(this.requestId).subscribe((data) => {
          this.request = data;
          this.request.dateRequested = data.dateRequested.substring(0, 10);
          if (this.request.requestTypeId === "REQ-T-002") {
            this.STR_DOWNLOAD_FORM = "FAQ";
          }
          else {
            this.STR_DOWNLOAD_FORM = "Download Form";
          }
        })
      }
    });
  }

  saveRequest() {
    this.isSaveAction = true;
  }

  submitRequest(event: Event, form: NgForm) {

    this.isRunning = true;
    let newFormData = new FormData();
    let newRequest: Request = form.value;
    let msg: string;
    newRequest.username = this.currentUser.username;
    newRequest.requestTypeId = this.request.requestTypeId
    newRequest.requestId = this.requestId;

    if (this.isSaveAction) {
      newRequest.state = "SAVED";
      msg = this.STR_STATUS_SAVED;
    }
    else {
      newRequest.state = "REQUESTED";
      msg = this.STR_STATUS_REQUESTED;
    }

    for (var key in newRequest) {
      newFormData.append(key, newRequest[key])
    }

    if (this.selectedFiles) {
      for (var i in this.selectedFiles) {
        newFormData.append("attachment", this.selectedFiles[i]);
      }
    }

    if (!this.isSavedRequest) {
      this.requestService.createRequest(newFormData).subscribe((data: any) => {
        this.isRunning = false;

        if (this.isSaveAction) {
          this.isSaveAction = false;
          this.router.navigate([`/requests/saved/${data.requestId}`]);
        }
        $.notify({
          icon: "notifications",
          message: `Change Request <strong><a href="/requests/${data.requestId}"> ${data.requestId}</a></strong> ${msg}!`

        }, {
            placement: {
              from: "bottom",
              align: "right"
            }
          });
      }, (error: any) => {
        this.isRunning = false;
        ErrorHandler.showErrorMessages(error.error);
      });
    }
    else {
      this.requestService.saveRequest(newRequest.requestId, newFormData).subscribe((data) => {
        this.isRunning = false;
        this.isSaveAction = false;
        $.notify({
          icon: "notifications",
          message: `Change Request <strong><a href="/requests/${newRequest.requestId}"> ${newRequest.requestId}</a></strong> ${msg}!`

        }, {
            placement: {
              from: "bottom",
              align: "right"
            }
          });
      }, (error: any) => {
        this.isRunning = false;
        ErrorHandler.showErrorMessages(error.error);
      })
    }

  }

  fileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {

      this.attachments = [];
      this.selectedFiles = [];
      for (var i = 0; i < fileList.length; i++) {
        var file: File = fileList[i];
        // var attachment: AttachmentModel = {
        //   fileName: file.name,
        //   fileType: file.type,
        //   location: `${this.projectId}/`
        // }
        //this.attachments.push(attachment);
        this.selectedFiles.push(file);
      }
      this.btnAttachForm.nativeElement.innerHTML = `${file.name}(${Math.round(file.size / (1024))}KB)`;
    }
    else {
      this.btnAttachForm.nativeElement.innerHTML = this.STR_ATTACH_FORM;
    }
  }
  requestTypeChanged(event) {
    this.requestService.getRequestType(event.value).subscribe((data) => {
      this.requestTypeFormPath = data.formPath;
    });
  }
  downloadForm(path: string) {
    window.open(path);
  }
}
