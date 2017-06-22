using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGrande.Models
{
    public class CustomerProfile
    {
        public int CustomerProfileId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
