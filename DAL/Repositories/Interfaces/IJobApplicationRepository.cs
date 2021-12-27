using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using QuickApp.Helpers;

namespace DAL.Repositories.Interfaces
{
    public interface IJobApplicationRepository : IRepository<JobApplications>
    {
        void AddJobApplication(JobApplications jobApplication);
        IEnumerable<JobApplications> GetAllJobApplications(string status);
        JobApplications GetJobApplicationDetail(int id);
        void UpdateJobApplication(JobApplications jobApplication);
        FileResult DownloadFile(int id);
        void AddJobApplicationNotes(JobApplicationNotes applicationNotes);
        void UpdateJobApplicationNotes(JobApplicationNotes applicationNotes);
        void DeleteJobApplicationNotes(int id);
    }
}
