using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.ViewModels
{
    public class JobViewModel
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
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string AttachmentFile { get; set; }
        public List<JobNotesViewModel> JobApplicationNotes { get; set; }
    }
}
