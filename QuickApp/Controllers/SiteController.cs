using AutoMapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;


        public SiteController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IEmailSender emailSender)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var allCustomers = _unitOfWork.Sites.GetAllSiteData();
            return Ok(_mapper.Map<IEnumerable<SiteViewModel>>(allCustomers));
        }

        [HttpPost]
        public IActionResult Post([FromBody] SiteViewModel value)
        {
            var site = _mapper.Map<Site>(value);
            site.CreatedBy = User.Identity.Name;
            site.UpdatedBy = User.Identity.Name;
            site.CreatedDate = DateTime.UtcNow;
            site.UpdatedDate = DateTime.UtcNow;
            _unitOfWork.Sites.AddSite(site);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SiteViewModel value)
        {
            var site = _mapper.Map<Site>(value);
            site.UpdatedBy = User.Identity.Name;
            site.UpdatedDate = DateTime.UtcNow;
            _unitOfWork.Sites.UpdateSite(site);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.Sites.DeleteSite(id);
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}
