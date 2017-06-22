using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGrande.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime DateBooked { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfPeople { get; set; }
        public double TotalPrice { get; set; }


        public int PackageId { get; set; }
        public string PackageName { get; set; }

        public TravelPackage TravelPackage { get; set; }

        public string UserId { get; set; }


        public bool LeftFeedback { get; set; }


    }
}
