import { Component, OnInit } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { RequestType } from '../../models/requestType.model';
import { Request } from '../../models/request.model'
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
    let newRequest: Request = form.value;
    newRequest.username = this.currentUser.username;

    this.requestService.createRequest(newRequest).subscribe((data: any) => {
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

}
