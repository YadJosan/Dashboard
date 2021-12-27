import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { ConfigurationService } from './configuration.service';
import { EndpointBase } from './endpoint-base.service';

@Injectable({
  providedIn: 'root'
})
export class JobEndpointService extends EndpointBase {
  get jobApplicationUrl() { return this.configurations.baseUrl + '/api/JobApplication'; }  

  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  getApplications<T>(status:string): Observable<T> {
    return this.http.get<T>(this.jobApplicationUrl+'?status='+status, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  getApplicationDetail<T>(id:number): Observable<T> {
    return this.http.get<T>(this.jobApplicationUrl+'/getjobdetail?id='+id, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  updateJobApplication<T>(jobObject: any): Observable<T> {
    return this.http.put<T>(this.jobApplicationUrl,JSON.stringify(jobObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  downloadAttachment<T>(id:number): Observable<T> {
    return this.http.get<T>(this.jobApplicationUrl+'/downloadfile/'+id, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  saveJobApplicationNote<T>(noteObject: any): Observable<T> {
    return this.http.post<T>(this.jobApplicationUrl+'/savejobnotes',JSON.stringify(noteObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  deleteJobApplicationNote<T>(id: number): Observable<T> {
    return this.http.delete<T>(this.jobApplicationUrl+'/deletejobnotes?id='+id, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }

  updateJobApplicationNote<T>(noteObject: any): Observable<T> {
    return this.http.put<T>(this.jobApplicationUrl+'/updatejobnotes',JSON.stringify(noteObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => null);
      }));
  }
}
