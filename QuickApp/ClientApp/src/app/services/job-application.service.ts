import { Injectable } from '@angular/core';
import { JobApplication } from '../models/jobApplication.model';
import { JobEndpointService } from './job-endpoint.service';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationService {

  constructor(
    private jobEndpointService: JobEndpointService) {
  }

  getJobApplications(status:string) {
    return this.jobEndpointService.getApplications<JobApplication[]>(status);
  }

  getJobApplicationDetail(id:number) {
    return this.jobEndpointService.getApplicationDetail<JobApplication>(id);
  }

  updateJobApplication(jobObject:any) {
    return this.jobEndpointService.updateJobApplication<boolean>(jobObject);
  }

  downloadAttachment(id:number) {
    return this.jobEndpointService.downloadAttachment<any>(id);
  }

  saveJobApplicationNote(noteObject:any) {
    return this.jobEndpointService.saveJobApplicationNote<boolean>(noteObject);
  }

  deleteJobApplicaionNote(id:number) {
    return this.jobEndpointService.deleteJobApplicationNote<boolean>(id);
  }
  
  updateJobApplicationNote(noteObject:any) {
    return this.jobEndpointService.updateJobApplicationNote<boolean>(noteObject);
  }
}
