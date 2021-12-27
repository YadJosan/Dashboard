import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Board } from 'src/app/models/board.model';
import { Column } from 'src/app/models/column.model';
import { JobApplication } from 'src/app/models/jobApplication.model';
import { JobApplicationService } from 'src/app/services/job-application.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem, CdkDropList } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-job-applications',
  templateUrl: './job-applications.component.html',
  styleUrls: ['./job-applications.component.scss']
})
export class JobApplicationsComponent implements OnInit {
  appliedJobApplications:JobApplication[] = [];
  scheduledJobApplications:JobApplication[] = [];
  hiredJobApplications:JobApplication[] = [];
  rejectedJobApplications:JobApplication[] = [];
  displayedColumns: string[] = ['First', 'Last', 'Email', 'Cell Phone', 'Job Title', 'Job Location','Details'];  

  public board: Board = new Board('Test Board', []);
  isKanbanView = true;

  constructor(private jobApplicationService:JobApplicationService,
    private router: Router) { }

  ngOnInit(): void {
    this.getAppliedJobApplications();
    this.getScheduledJobApplications();
    this.getHiredJobApplications();
    this.getRejectedJobApplications();
  }

  getAppliedJobApplications() {
    this.jobApplicationService.getJobApplications("Applied").subscribe(res=>{          
      this.appliedJobApplications = res;
      this.board.columns.push(new Column('Applied', 1, this.appliedJobApplications));
    });
  }

  getScheduledJobApplications() {
    this.jobApplicationService.getJobApplications("Scheduled interview").subscribe(res=>{          
      this.scheduledJobApplications = res;
      this.board.columns.push(new Column('Scheduled interview', 2, this.scheduledJobApplications));
    });
  }

  getHiredJobApplications() {
    this.jobApplicationService.getJobApplications("Hired").subscribe(res=>{          
      this.hiredJobApplications = res;
      this.board.columns.push(new Column('Hired', 3, this.hiredJobApplications));
    });
  }

  getRejectedJobApplications() {
    this.jobApplicationService.getJobApplications("Rejected").subscribe(res=>{          
      this.rejectedJobApplications = res;
      this.board.columns.push(new Column('Rejected', 4, this.rejectedJobApplications));
    });
  }

  gotoJobDetail(id:number){
    this.router.navigate([`/job-detail/${id}`]);
  }

  public dropGrid(event: CdkDragDrop<JobApplication[]>): void {
    moveItemInArray(this.board.columns, event.previousIndex, event.currentIndex);
  }

  public drop(event: CdkDragDrop<JobApplication[]>,): void {    
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      event.previousContainer.data[event.previousIndex].status = event.container.id == "1" ?"Applied":event.container.id == "2"?"Scheduled interview":event.container.id == "3"?"Hired":"Rejected";
      this.updateJobStatus(event.previousContainer.data[event.previousIndex]);
      transferArrayItem(event.previousContainer.data,
          event.container.data,
          event.previousIndex,
          event.currentIndex);      
    }
  }

  boardOrder(){
    return this.board.columns.sort((a, b) => a.id - b.id);
  }

  updateJobStatus(jobDetail:any){
    this.jobApplicationService.updateJobApplication(jobDetail).subscribe(res=>{         
         
    });
  }

  gotoView(){
    this.isKanbanView = !this.isKanbanView;
  }
  
}
