import { Component, OnInit } from '@angular/core';
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
  request: Request;
  currentUser: any;
  attachments: Attachment[];
  selectedFiles: File[];
  constructor(private requestService: RequestService) { }

  ngOnInit() {
    this.request = new Request();
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
    }
  }
}
