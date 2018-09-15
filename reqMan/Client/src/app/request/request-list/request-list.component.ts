import { Component, OnInit } from '@angular/core';
import { RequestType } from '../../models/requestType.model';
import { RequestService } from '../../services/request.service';
import { Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {

  requestList = [] as RequestType[];
  displayedColumns: string[] = ['requestId', 'requestType', 'user', 'state', 'created', 'actions']

  approveCR(reqObj: any) {
    var changeReq = {
      state: "APPROVED",
      requestId: reqObj.requestId,
      username: reqObj.username,
      requestTypeId: reqObj.requestTypeId
    }
    this.requestService.updateRequest(changeReq).subscribe((data: any) => {
      $.notify({
        icon: "notifications",
        message: `Change Request ${reqObj.requestId} updated!`

      }, {
          placement: {
            from: "bottom",
            align: "right"
          }
        });
      this.router.navigate(['/requests']);
    })
  }

  rejectCR(reqObj: any) {
    var changeReq = {
      state: "REJECTED",
      requestId: reqObj.requestId,
      username: reqObj.username,
      requestTypeId: reqObj.requestTypeId
    }
    this.requestService.updateRequest(changeReq).subscribe((data: any) => {
      $.notify({
        icon: "notifications",
        message: `Change Request ${reqObj.requestId} updated!`

      }, {
          placement: {
            from: "bottom",
            align: "right"
          }
        });
      this.router.navigate(['/requests']);
    })


  }

  constructor(private requestService: RequestService, private router: Router) { }

  ngOnInit() {
    this.requestService.getRequests().subscribe((data: any) => {
      this.requestList = data;
    });
  }
}
