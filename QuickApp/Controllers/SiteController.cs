using AutoMapper;
using DAL;
using DAL.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using QuickApp.ViewModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuickApp.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<PandaDocSetting> _pandadocConfig;


        public SiteController(IMapper mapper, IUnitOfWork unitOfWork, 
                              ILogger<CustomerController> logger, 
                              IEmailSender emailSender, IOptions<PandaDocSetting> pandadocConfig)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
            _pandadocConfig = pandadocConfig;
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

        [HttpGet("doclist")]
        public async Task<IActionResult> GetDocumentList()
        {

            var strResponse = await HttpRequestHelper.GetRequestAsync($"{_pandadocConfig.Value.BaseApiUrl}{"/public/v1/documents"}",
                                                            new AuthenticationHeaderValue("API-Key", _pandadocConfig.Value.ApiKey));

            return Ok(_mapper.Map<HttpResponseDto>(strResponse));
        }

        [HttpPost("createdocument")]
        public async Task<IActionResult> CreateDocument([FromForm] CreateDocumentDto createDocument)
        {
            //Request Body
            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("file", createDocument.file),
                new KeyValuePair<string, string>("data", createDocument.data)
            };

            var strResponse = await HttpRequestHelper.PostRequestAsync($"{_pandadocConfig.Value.BaseApiUrl}{"/public/v1/documents"}",
                                                            new AuthenticationHeaderValue("API-Key", _pandadocConfig.Value.ApiKey),
                                                            createDocument);

            return Ok(_mapper.Map<HttpResponseDto>(strResponse));
        }
    }
}
