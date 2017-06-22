using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelGrande.Models;

namespace TravelGrande.ViewModels
{
    public class DisplayAllPackagesPerProviderVM
    {
        public IEnumerable<TravelPackage> Packages { get; set; }
        public string ProviderName { get; set; }


    }
}
