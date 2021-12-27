using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuickApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JobApplicationRepository : Repository<JobApplications>, IJobApplicationRepository
    {
        public JobApplicationRepository(DbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public IEnumerable<JobApplications> GetAllJobApplications(string status = null)
        {
            if (string.IsNullOrEmpty(status))
            {
                return _appContext.JobApplications.ToList();
            }
            else
            {
                return _appContext.JobApplications.Where(j => j.Status == status.Trim()).ToList();
            }
        }

        public JobApplications GetJobApplicationDetail(int id)
        {
            return _appContext.JobApplications.Where(j => j.Id == id)
                .Include(x=>x.JobApplicationNotes).ThenInclude(u=>u.User)                
                .FirstOrDefault();
        }

        public void AddJobApplication(JobApplications jobApplication)
        {
            _appContext.Add(jobApplication);
        }

        public void AddJobApplicationNotes(JobApplicationNotes applicationNotes)
        {
            _appContext.Add(applicationNotes);
        }

        public void UpdateJobApplicationNotes(JobApplicationNotes applicationNotes)
        {
            _appContext.Update(applicationNotes);
        }

        public void DeleteJobApplicationNotes(int id)
        {
            var jobApplication = _appContext.JobApplicationNotes.Where(x => x.Id == id).FirstOrDefault();
            _appContext.JobApplicationNotes.Remove(jobApplication);
        }

        public void UpdateJobApplication(JobApplications jobApplication)
        {
            _appContext.Update(jobApplication);
        }

        public FileResult DownloadFile(int id)
        {
            var fileAttachment = _appContext.JobApplications.AsNoTracking().FirstOrDefault(j => j.Id == id);

            if (fileAttachment == null) throw new ValidationException("Record does not exist");
            if (string.IsNullOrEmpty(fileAttachment.Attachment)) throw new ValidationException("File Path does not exist");
            string filePath = fileAttachment.Attachment;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //Read the File into a Byte Array.
            if (!File.Exists(filePath)) throw new ValidationException("File does not exist");
            string fileName = filePath.Split("JobApplicationFiles/")[1];
            byte[] bytes = File.ReadAllBytes(filePath);
            string fileType = MimeMapping.MimeUtility.GetMimeMapping(fileName);
            return new FileResult(fileName, fileType, bytes);

        }
    }
}
