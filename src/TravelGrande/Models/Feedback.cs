using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGrande.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }


        public int TravelPackageId { get; set; }
        public TravelPackage TravelPackage { get; set; }

        public string MyUserId { get; set; }
        

        public string UserName { get; set; }
    }
}
