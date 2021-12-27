using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class JobApplications
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public string Attachment { get; set; }        
        public string FirstName { get; set; }
        public string PrefferdName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Status { get; set; }        
        public DateTime Date { get; set; }
        
        public List<JobApplicationNotes> JobApplicationNotes { get; set; }
    }
}
