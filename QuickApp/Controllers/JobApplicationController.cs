using AutoMapper;
using DAL;
using DAL.Core.Interfaces;
using DAL.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileResult = QuickApp.Helpers.FileResult;

namespace QuickApp.Controllers
{
    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]    
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _hostEnvironment;
        private readonly IAccountManager _accountManager;


        public JobApplicationController(IMapper mapper,
                              IUnitOfWork unitOfWork,
                              ILogger<CustomerController> logger,
                              IEmailSender emailSender,
                              IWebHostEnvironment environment,
                              IAccountManager accountManager
                              )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
            _hostEnvironment = environment;
            _accountManager = accountManager;
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult Get(string status)
        {
            var allJobApplications = _unitOfWork.JobApplication.GetAllJobApplications(status);
            return Ok(_mapper.Map<IEnumerable<JobViewModel>>(allJobApplications));
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("getjobdetail")]
        public IActionResult GetJobDetail(int id)
        {
            var jobApplicationsDetail = _unitOfWork.JobApplication.GetJobApplicationDetail(id);
            return Ok(_mapper.Map<JobViewModel>(jobApplicationsDetail));
        }

        [HttpPost]
        public IActionResult SaveJobs([FromBody] JobViewModel value)
        {
            string filepath = ConvertByteToFile(value.AttachmentFile,value.Attachment);
            var jobApplication = _mapper.Map<JobApplications>(value);
            jobApplication.Date = DateTime.Now;
            jobApplication.Attachment = filepath;
            jobApplication.Status = "Applied"; //Scheduled interview/Hired/Rejected/Future consideration

            _unitOfWork.JobApplication.AddJobApplication(jobApplication);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpPut]
        public IActionResult UpdateJobStatus([FromBody] JobViewModel value)
        {            
            var jobApplication = _mapper.Map<JobApplications>(value);

            _unitOfWork.JobApplication.UpdateJobApplication(jobApplication);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("savejobnotes")]
        public async Task<IActionResult> SaveJobNotes([FromBody] JobNotesViewModel value)
        {
            ApplicationUser appUser = await _accountManager.GetUserByIdAsync(value.UserId);
            var jobApplicationNotes = _mapper.Map<JobApplicationNotes>(value);
            jobApplicationNotes.Date = DateTime.Now;
            jobApplicationNotes.User = appUser;

            _unitOfWork.JobApplication.AddJobApplicationNotes(jobApplicationNotes);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("updatejobnotes")]
        public async Task<IActionResult> UpdateJobNotes([FromBody] JobNotesViewModel value)
        {
            ApplicationUser appUser = await _accountManager.GetUserByIdAsync(value.UserId);
            var jobApplicationNotes = _mapper.Map<JobApplicationNotes>(value);
            jobApplicationNotes.Date = DateTime.Now;
            jobApplicationNotes.User = appUser;

            _unitOfWork.JobApplication.UpdateJobApplicationNotes(jobApplicationNotes);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("deletejobnotes")]
        public IActionResult DeleteJobNotes(int id)
        {
            _unitOfWork.JobApplication.DeleteJobApplicationNotes(id);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("downloadfile/{id}")]
        public FileResult DownloadFile(int id)
        {
            var resfile = _unitOfWork.JobApplication.DownloadFile(id);
            return resfile;
        }

        private string ConvertByteToFile(string b64Str,string filename)
        {
            b64Str = b64Str.Split("base64,").Count() > 1 ? b64Str.Split("base64,")[1] : "";
            string hostPath = _hostEnvironment.WebRootPath+ "/JobApplicationFiles";
            string path = Path.Combine(hostPath, filename);
            Byte[] bytes = Convert.FromBase64String(b64Str);
            System.IO.File.WriteAllBytes(path, bytes);

            return path;
        }

        private async Task<UserViewModel> GetUserViewModelHelper(string userId)
        {
            var userAndRoles = await _accountManager.GetUserAndRolesAsync(userId);
            if (userAndRoles == null)
                return null;

            var userVM = _mapper.Map<UserViewModel>(userAndRoles.Value.User);
            userVM.Roles = userAndRoles.Value.Roles;

            return userVM;
        }
    }
}
