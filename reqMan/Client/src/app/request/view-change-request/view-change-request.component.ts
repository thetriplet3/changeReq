import { Component, OnInit } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { Request } from '../../models/request.model'
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-view-change-request',
  templateUrl: './view-change-request.component.html',
  styleUrls: ['./view-change-request.component.scss']
})
export class ViewChangeRequestComponent implements OnInit {

  request: Request;
  currentUser: any;
  constructor(private requestService: RequestService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.request = new Request();
    this.route.paramMap.subscribe((paramMap: ParamMap) => {
      if (paramMap.has('requestId')) {
        var requestId = paramMap.get('requestId');
        this.requestService.getRequest(requestId).subscribe((data: any) => {
          this.request = data;
          this.request.dateRequested = data.dateRequested.substring(0, 10);
          this.request.dateModified = data.dateModified.substring(0, 10);
        });

      }
    });
  }

}
