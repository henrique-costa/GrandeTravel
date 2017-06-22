using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelGrande.Models;
namespace TravelGrande.ViewModels
{
    public class DisplayBookingVM
    {
        public IEnumerable<Booking> listOfBooking { get; set; }
    }
}
