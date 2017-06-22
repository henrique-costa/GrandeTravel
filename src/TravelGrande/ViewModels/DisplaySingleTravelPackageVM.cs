using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelGrande.Models;

namespace TravelGrande.ViewModels
{
    public class DisplaySingleTravelPackageVM
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public bool Discontinued { get; set; }
        public string PackPhotoPath { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public string ProviderName { get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }
        public Feedback Feedback { get; set; }

    }
}
