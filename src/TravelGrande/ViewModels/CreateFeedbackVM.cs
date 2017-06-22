using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelGrande.ViewModels
{
    public class CreateFeedbackVM
    {
        [Required]
        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TravelPackageId { get; set; }
        public int BookingId { get; set; }
    }
}
