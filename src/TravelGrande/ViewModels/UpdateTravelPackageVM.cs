using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelGrande.ViewModels
{
    public class UpdateTravelPackageVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string State { get; set; }

        [Display(Name = "Discontinue package? ")]
        public bool Discontinued { get; set; }



    }
}
