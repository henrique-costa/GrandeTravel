using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelGrande.ViewModels
{
    public class CreateTravelPackageVM
    {
        [Required, Display(Name = "Package Name")]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public string ProviderName { get; set; }
    }
}
