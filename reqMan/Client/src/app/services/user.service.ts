import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { User } from '../models/user.model';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  selectedUser: User;

  constructor(private http: HttpClient) { }

  userAuthentication(user: User) {
    var body = JSON.stringify(user);

    var headerOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.http.post('/api/Users/Authenticate', body, {headers : headerOptions});
  }

  getUsers() {
    return this.http.get<User>('/api/Users');
  }
  
  getUser(id) {
    return this.http.get<User>(`/api/Users/${id}`);
  }

  createUser(userObj) {
    
    return this.http.post<User>(`/api/Users/`, userObj);
  }
  updateUser(userObj) {
    return this.http.put<User>(`/api/Users/${userObj.userId}`, userObj);
  }
}
