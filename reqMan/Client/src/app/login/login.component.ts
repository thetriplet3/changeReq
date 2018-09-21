import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    constructor(public userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.selectedUser = {
      username: '',
      password: ''
    }
    localStorage.setItem('currentUser', null);
  }
  onSubmit(form : NgForm){
    this.userService.userAuthentication(form.value).subscribe((data : any) => {
      localStorage.setItem('userToken', data.token);
      localStorage.setItem('currentUser', JSON.stringify(data.user));
      this.router.navigate(['/dashboard'])
    });
  }
}
