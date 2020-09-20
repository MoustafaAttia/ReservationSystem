using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Chair : RoomResources
    {
        public int ChairId { get; private set; }

        public Chair()
        {
            this.IsMoveable = false;
        }

        public Chair(int charId)
        {
            this.ChairId = ChairId;
            this.IsMoveable = false;
        }
    }
}
