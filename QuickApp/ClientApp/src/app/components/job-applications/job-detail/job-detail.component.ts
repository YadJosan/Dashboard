import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { JobApplication, JobApplicationNotes, JobStatus } from 'src/app/models/jobApplication.model';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';
import { JobApplicationService } from 'src/app/services/job-application.service';

@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.scss']
})
export class JobDetailComponent implements OnInit {
  jobDetail:any={};
  jobId:number;
  deleteNoteId: number;
  updateNoteId: number;
  currentUserId: string;
  jobStatus: JobStatus[] = [
    {value: 'Applied', name: 'Applied'},
    {value: 'Scheduled interview', name: 'Scheduled interview'},
    {value: 'Hired', name: 'Hired'},
    {value: 'Rejected', name: 'Rejected'}
  ];

  formResetToggle = true;
  addNote: JobApplicationNotes = new JobApplicationNotes();
  jobApplicationNotes: JobApplicationNotes[];

  @ViewChild('addNoteModal', { static: true })
  addNoteModal: ModalDirective;
  @ViewChild('deleteNoteModal', { static: true })
  deleteNoteModal: ModalDirective;

  constructor(private alertService: AlertService, 
    private jobApplicationService:JobApplicationService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {    
    this.addNote.note = "";
    this.currentUserId = this.authService.currentUser.id;
    this.route.params.subscribe( params => {
      this.jobId = params["id"];
      this.getJobApplicationDetail(this.jobId)
    });   
  }

  getJobApplicationDetail(id:number) {
    this.jobApplicationService.getJobApplicationDetail(id).subscribe(res=>{          
      this.jobDetail = res; 
      this.jobApplicationNotes = this.jobDetail.jobApplicationNotes;
      console.log(this.jobDetail,'this.jobApplicationsDetail')
    });
  }

  getAtachmentName() {        
    return this.jobDetail.attachment != undefined ? this.jobDetail.attachment.split("JobApplicationFiles/")[1] : "";
  }

  gotoList(){
    this.router.navigate(["/job-applications"]);
  }

  saveJob(){
    this.jobApplicationService.updateJobApplication(this.jobDetail).subscribe(res=>{         
        this.router.navigate(["/job-applications"]);    
    });
  }

  downloadAtachmentName() {
    this.jobApplicationService.downloadAttachment(this.jobDetail.id).subscribe(res=>{
      if (res) {
        this.downloadFile(res.type, res.bytes, res.name);
      }            
    });
  }

  downloadFile(filetype, base64, filename) {
    const linkSource = `data:${filetype};base64,${base64}`;
    const downloadLink = document.createElement('a');
    const fileName = filename;

    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  addNoteHandler() {
    this.addNote.note = "";
    this.addNote.id = 0; // set it to null, because when user clicks on edit and do nothing, note id is set to addNote.id
    this.addNoteModal.show();
  }

  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  saveNote() {
    this.addNote.userId = this.authService.currentUser.id;
    this.addNote.jobApplicationId = this.jobDetail.id;

    if(!this.addNote.id) {
      this.jobApplicationService.saveJobApplicationNote(this.addNote).subscribe(res=>{          
        this.getJobApplicationDetail(this.jobDetail.id);
        this.addNote.note = "";
        this.addNoteModal.hide();
      });
    } else {
      this.jobApplicationService.updateJobApplicationNote(this.addNote).subscribe(res=>{
        this.getJobApplicationDetail(this.jobDetail.id);
        this.addNote.note = "";
        this.addNote.id = null;
        this.addNoteModal.hide();
      })
    }
  }

  confirmDeleteNote(id:number) {
    this.deleteNoteModal.show();
    this.deleteNoteId = id;
  }

  deleteNote() {
    this.jobApplicationService.deleteJobApplicaionNote(this.deleteNoteId).subscribe(res => {
      this.getJobApplicationDetail(this.jobDetail.id);
      this.deleteNoteModal.hide();
    });
  }

  editNote(note) {
    this.addNote.note = note.note;
    this.addNote.id = note.id;
    this.addNoteModal.show();
  }

  formatAMPM(date) {
    var jsDate = new Date(date);
    var day = jsDate.getDate();
    var month = jsDate.getMonth() + 1;
    var year = jsDate.getFullYear();
    var hours = jsDate.getHours();
    var minutes = jsDate.getMinutes().toString();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = +minutes < 10 ? '0'+minutes : minutes;
    var strTime = day + "/" + month + '/' + year + ' ' +hours + ':' + minutes + ' ' + ampm;
    return strTime;
  }
}
