import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { User } from 'app/models/user.model';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {

  user: User;
  title: string;
  isRunning: true;

  constructor(private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe((paramMap: ParamMap) => {
      if (paramMap.has('username')) {
        var username = paramMap.get('username');
        this.userService.getUser(username).subscribe((data) => {
          this.user = data;
        })

      }
    })
  }

}
