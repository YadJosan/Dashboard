using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.ViewModels
{
    public class JobNotesViewModel
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }        
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
