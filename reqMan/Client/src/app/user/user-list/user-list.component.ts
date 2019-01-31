import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { FormControl } from '@angular/forms';
import { User, UserType } from 'app/models/user.model';
import { UserService } from 'app/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  userList = [] as User[];
  userTypes: UserType[] = [];
  displayedColumns: string[] = ['username', 'userType', 'email', 'firstName', 'lastName', 'actions'];
  currentUser: any;
  isLoading: boolean;
  isRunning: boolean;
  dataSource: MatTableDataSource<User>;

  usernameFilter = new FormControl();
  userTypeIdFilter = new FormControl();
  globalFilter = '';

  filteredValues = {
    username: '',
    userTypeId: '',
    state: ''
  };

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userTypes = [
      {name: "ADMIN", userTypeId: "ADMIN"},
      {name: "USER", userTypeId: "USER"}
    ]
    this.isLoading = true;
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.userService.getUsers().subscribe((data: any) => {
      this.userList = data;
      this.userList['isRunning'] = false;
      this.isLoading = false;
      this.dataSource = new MatTableDataSource(this.userList);
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      })

      this.usernameFilter.valueChanges.subscribe((usernameFilterValue) => {
        this.filteredValues['username'] = usernameFilterValue;
        this.dataSource.filter = JSON.stringify(this.filteredValues);
      });

      this.userTypeIdFilter.valueChanges.subscribe((userTypeIdValue) => {
        this.filteredValues['userTypeId'] = userTypeIdValue;
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

  processUser(reqObj: any, action: string) {

  }

  refresh() {
    this.userService.getUsers().subscribe((data: any) => {
      this.userList = data;
      this.userList['isRunning'] = false;
    });
  }

  customFilterPredicate() {
    const myFilterPredicate = (data: User, filter: string): boolean => {
      var globalMatch = !this.globalFilter;

      if (this.globalFilter) {
        // search all text fields
        globalMatch = data.toString().trim().toLowerCase().indexOf(this.globalFilter.toLowerCase()) !== -1;
      }

      if (!globalMatch) {
        return;
      }

      let searchString = JSON.parse(filter);
      return data.username.toString().toLocaleLowerCase().trim().indexOf(searchString.username.toLowerCase()) !== -1 &&
        data.userType.toString().trim().indexOf(searchString.userTypeId) !== -1;
    }
    return myFilterPredicate;
  }

}
