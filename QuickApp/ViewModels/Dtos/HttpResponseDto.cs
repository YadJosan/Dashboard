using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QuickApp.ViewModels.Dtos
{
    public class HttpResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}
