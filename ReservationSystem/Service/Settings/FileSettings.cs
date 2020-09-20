using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Service.Settings
{
    public class FileSettings
    {
        public string RootDir { get; set; }
        public string OfficesFileName { get; set; }
        public string RoomsFileName { get; set; }
        public string ReservationsFileName { get; set; }
    }
}
