import { Component, OnInit } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { Request } from '../../models/request.model'
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RequestType } from '../../models/requestType.model';
import { User } from '../../models/user.model';
declare var $: any;

@Component({
  selector: 'app-view-change-request',
  templateUrl: './view-change-request.component.html',
  styleUrls: ['./view-change-request.component.scss']
})
export class ViewChangeRequestComponent implements OnInit {

  request: Request;
  currentUser: any;
  isRunning: boolean = true;
  isAfter10th: boolean = false;

  constructor(private requestService: RequestService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.request = new Request();
    this.request.requestType = new RequestType();
    this.request.user = new User();
    this.currentUser = JSON.parse(localStorage.getItem('currentUser')); 
    this.route.paramMap.subscribe((paramMap: ParamMap) => {
      if (paramMap.has('requestId')) {
        var requestId = paramMap.get('requestId');
        this.requestService.validateRequest(requestId).subscribe((res) =>{
          this.isAfter10th = res.isAfter10th;
        })
        this.requestService.getRequest(requestId).subscribe((data: any) => {
          this.request = data;
          this.request.dateRequested = data.dateRequested.substring(0, 10);
          this.request.dateModified = data.dateModified.substring(0, 10);
          this.isRunning = false;
        });

      }
    });
  }

  processCR(reqObj: any, action: string) {
    this.isRunning = true;
    var changeReq = {
      state: action,
      requestId: reqObj.requestId,
      username: reqObj.username,
      requestTypeId: reqObj.requestTypeId,
      dateRequested: reqObj.dateRequested
    }
    this.requestService.updateRequest(Object.assign(reqObj, changeReq)).subscribe((data: any) => {
      this.refresh();
      $.notify({
        icon: "notifications",
        message: `Change Request ${reqObj.requestId} updated!`

      }, {
          placement: {
            from: "bottom",
            align: "right"
          }
        });
    })
  }
  refresh() {
    this.requestService.getRequest(this.request.requestId).subscribe((data: any) => {
      this.request = data;
      this.request.dateRequested = data.dateRequested.substring(0, 10);
      this.request.dateModified = data.dateModified.substring(0, 10);
      this.isRunning = false;
    });
  }
  downloadForm(path: string) {
    window.open(path);
  }

}
