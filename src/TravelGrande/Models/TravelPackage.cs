using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGrande.Models
{
    public class TravelPackage
    {
        public int TravelPackageId { get; set; }
        public string Name { get; set; }

        // local
        public string Location { get; set; }
        public string State { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }

        // toggle to continue or discontinue a package
        public bool Discontinued { get; set; }

        public string ProviderName { get; set; }

        public string PhotoPath { get; set; } //add-migration after doing this
        
        //used to identify which travel PROVIDER created the package
        public string UserId { get; set; }       


        //one TravelPackage has many Bookings and/or feedbacks
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        



    }
}
