import { Component, OnInit, ViewChild } from '@angular/core';
import { Request } from '../../models/request.model';
import { RequestService } from '../../services/request.service';
import { Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { RequestType } from '../../models/requestType.model';
import { FormControl } from '@angular/forms';
declare var $: any;

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})

export class RequestListComponent implements OnInit {

  console = console;
  requestList = [] as Request[];
  requestTypes: RequestType[] = [];
  displayedColumns: string[] = ['requestId', 'requestType', 'user', 'state', 'created', 'actions'];
  currentUser: any;
  isLoading: boolean;
  isRunning: boolean;
  dataSource: MatTableDataSource<Request>;

  requestIdFilter = new FormControl();
  requestTypeIdFilter = new FormControl();
  globalFilter = '';

  filteredValues = {
    requestId: '',
    requestTypeId: '',
    state: ''
  };

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private requestService: RequestService, private router: Router) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.requestService.getRequestTypes().subscribe((data: any) => {
      this.requestTypes = data;
    });
    this.requestService.getRequests().subscribe((data: any) => {
      this.requestList = data;
      this.requestList['isRunning'] = false;
      this.isLoading = false;
      this.dataSource = new MatTableDataSource(this.requestList);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;

      this.requestIdFilter.valueChanges.subscribe((requestIdFilterValue) => {
        this.filteredValues['requestId'] = requestIdFilterValue;
        this.dataSource.filter = JSON.stringify(this.filteredValues);
      });
  
      this.requestTypeIdFilter.valueChanges.subscribe((requestTypeIdValue) => {
        this.filteredValues['requestTypeId'] = requestTypeIdValue;
        this.dataSource.filter = JSON.stringify(this.filteredValues);
      });
  
      this.dataSource.filterPredicate = this.customFilterPredicate();
    });

  }

  applyFilter(filterValue: string) {
    this.globalFilter = filterValue;
    this.dataSource.filter = JSON.stringify(this.filteredValues);
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

  customFilterPredicate() {
    const myFilterPredicate = (data: Request, filter: string): boolean => {
      var globalMatch = !this.globalFilter;

      if (this.globalFilter) {
        // search all text fields
        globalMatch = data.toString().trim().toLowerCase().indexOf(this.globalFilter.toLowerCase()) !== -1;
      }

      if (!globalMatch) {
        return;
      }

      let searchString = JSON.parse(filter);
      return data.requestId.toString().toLocaleLowerCase().trim().indexOf(searchString.requestId.toLowerCase()) !== -1 &&
        data.requestTypeId.toString().trim().indexOf(searchString.requestTypeId) !== -1;
    }
    return myFilterPredicate;
  }

}