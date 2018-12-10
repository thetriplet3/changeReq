import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { RequestType } from '../../models/requestType.model';
import { Request, Attachment } from '../../models/request.model'
import { NgForm } from '@angular/forms';

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

  STR_ATTACH_FORM: string = "Attach Form"
  STR_UPLOAD_FORM: string = "Upload Form"

  constructor(private requestService: RequestService) { }

  @ViewChild('btnAttachForm') btnAttachForm : ElementRef;

  ngOnInit() {
    this.request = new Request();
    this.request.requestType = new RequestType();
    this.request.dateRequested = new Date().toISOString().substring(0, 10);
    this.requestService.getRequestTypes().subscribe((data: any) => {
      this.requestTypes = data;
    });
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  submitRequest(form: NgForm) {
    let newFormData = new FormData();
    let newRequest: Request = form.value;
    newRequest.username = this.currentUser.username;
    newRequest.requestTypeId = form.value.requestType.requestTypeId

    for (var key in newRequest) {
      newFormData.append(key, newRequest[key])
    }

    if (this.selectedFiles) {
      for (var i in this.selectedFiles) {
        newFormData.append("attachment", this.selectedFiles[i]);
      }
    }

    this.requestService.createRequest(newFormData).subscribe((data: any) => {
      $.notify({
        icon: "notifications",
        message: `Change Request <strong><a href="/requests/${data.requestId}"> ${data.requestId}</a></strong> created!`

      }, {
          placement: {
            from: "bottom",
            align: "right"
          }
        });
    });
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
      console.log(this.btnAttachForm.nativeElement);
      this.btnAttachForm.nativeElement.innerHTML = `${file.name}(${Math.round(file.size/(1024))}KB)`;
    }
    else {
      this.btnAttachForm.nativeElement.innerHTML = this.STR_ATTACH_FORM;
    }
  }
  requestTypeChanged(event) {
    this.requestTypeFormPath = event.value.formPath;
  }
  downloadForm(path: string) {
    window.open(path);
  }
}
