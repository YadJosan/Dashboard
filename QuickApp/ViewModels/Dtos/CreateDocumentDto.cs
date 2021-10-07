using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.ViewModels.Dtos
{
    public class CreateDocumentDto
    {        
        public string file { get; set; }
        public string data { get; set; }
    }
}
