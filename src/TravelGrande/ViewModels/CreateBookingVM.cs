using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace TravelGrande.ViewModels
{
    public class CreateBookingVM
    {

        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Number of People")]
        [Range(1, 20, ErrorMessage = "Please enter Atleast 1 Person and No more than 20 People")]
        public int NumberOfPeople { get; set; }

        public string TravelPackageName { get; set; }
        public int PackageId { get; set; }
        public double PackagePrice { get; set; }


    }
}
