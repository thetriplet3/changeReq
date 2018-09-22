import { Component, OnInit, ViewChild } from '@angular/core';
import { Request } from '../../models/request.model';
import { RequestService } from '../../services/request.service';
import { Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
declare var $: any;

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})

export class RequestListComponent implements OnInit {

  console = console;
  requestList = [] as Request[];
  displayedColumns: string[] = ['requestId', 'requestType', 'user', 'state', 'created', 'actions'];
  currentUser: any;
  isLoading: boolean;
  isRunning: boolean;
  dataSource: MatTableDataSource<Request>;


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private requestService: RequestService, private router: Router) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.requestService.getRequests().subscribe((data: any) => {
      this.requestList = data;
      this.requestList['isRunning'] = false;
      this.isLoading = false;
      this.dataSource = new MatTableDataSource(this.requestList);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  processCR(reqObj: any, action: string) {
    reqObj.isRunning = true;
    var changeReq = {
      state: action,
      requestId: reqObj.requestId,
      username: reqObj.username,
      requestTypeId: reqObj.requestTypeId,
      dateRequested: reqObj.dateRequested
    }
    this.requestService.updateRequest(changeReq).subscribe((data: any) => {
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
    this.requestService.getRequests().subscribe((data: any) => {
      this.requestList = data;
      this.requestList['isRunning'] = false;
    });
  }
}