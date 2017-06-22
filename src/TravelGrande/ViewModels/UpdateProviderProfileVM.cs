using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelGrande.ViewModels
{
    public class UpdateProviderProfileVM
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
