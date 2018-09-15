import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Request } from '../models/request.model'
import { RequestType } from '../models/requestType.model';



@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient) { }

  getRequests() {
    return this.http.get<Request>('https://localhost:44355/api/Requests');
  }
  getRequestTypes() {
    return this.http.get<RequestType>('https://localhost:44355/api/RequestTypes');
  }
  getRequest(id) {
    return this.http.get<Request>(`https://localhost:44355/api/Requests/${id}`);
  }

  createRequest(requestObj) {
    
    return this.http.post<Request>(`https://localhost:44355/api/Requests/`, requestObj);
  }
  updateRequest(requestObj) {
    return this.http.put<Request>(`https://localhost:44355/api/Requests/${requestObj.requestId}`, requestObj);
  }
}
