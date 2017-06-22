using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGrande.Models
{
    public class ProviderProfile
    {
        public int ProviderProfileId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
    }
}
