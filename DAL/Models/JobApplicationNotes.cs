using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class JobApplicationNotes
    {
        public int Id { get; set; }

        public int JobApplicationId { get; set; }
        public JobApplications JobApplication { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
