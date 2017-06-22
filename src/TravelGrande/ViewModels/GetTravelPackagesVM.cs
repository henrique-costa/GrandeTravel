using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;

using TravelGrande.Models;
namespace TravelGrande.ViewModels
{
    public class GetTravelPackagesVM
    {
        public IEnumerable<TravelPackage> TravelPackages { get; set; }

        public string LocationQuery { get; set; }
        
        [RegularExpression("^\\d")]
        public string MinPrice { get; set; }

        [RegularExpression("^\\d")]
        public string MaxPrice { get; set; }

        public string ResultMessage { get; set; }

        public int Total { get; set; }
    }
}
