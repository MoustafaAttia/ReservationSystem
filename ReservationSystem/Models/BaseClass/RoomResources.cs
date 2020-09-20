using ReservationSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class RoomResources
    {
        public int RoomId { get; set; }
        protected bool IsMoveable { get; set; }
        public State state { get; set; }
        public bool CurrentlyUsed { get; set; }
    }
}
