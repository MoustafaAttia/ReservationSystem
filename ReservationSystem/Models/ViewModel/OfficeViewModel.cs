using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.ViewModel
{
    public class OfficeViewModel
    {
        public List<Office> allOffices { get; set; }
        public Office office { get; set; }
        public List<string> Errors { get; set; }
    }
}
