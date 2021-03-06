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
    return this.http.get<Request>('/api/Requests');
  }
  getRequestTypes() {
    return this.http.get<RequestType>('/api/RequestTypes');
  }
  getRequestType(id) {
    return this.http.get<RequestType>(`/api/RequestTypes/${id}`);
  }
  getRequest(id) {
    return this.http.get<Request>(`/api/Requests/${id}`);
  }
  createRequest(requestObj) {
    return this.http.post<Request>(`/api/Requests/`, requestObj);
  }
  updateRequest(requestObj) {
    return this.http.put<Request>(`/api/Requests/${requestObj.requestId}`, requestObj);
  }
  saveRequest(requestId, requestObj) {
    return this.http.put<Request>(`/api/Requests/${requestId}/Save`, requestObj);
  }
  preValidateRequest() {
    return this.http.get<any>("/api/Requests/Validate");
  }
  validateRequest(id) {
    return this.http.get<any>(`/api/Requests/${id}/Validate`);
  }
}
