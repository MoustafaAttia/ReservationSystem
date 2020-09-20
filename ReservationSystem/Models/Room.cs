using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public List<RoomEquipment> Equipments { get; set; }
        public int OfficeId { get; set; }
        public bool HasChairs { get; set; }
    }
}
