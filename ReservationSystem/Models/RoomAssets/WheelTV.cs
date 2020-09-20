using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class WheelTV : RoomResources
    {
        public int WheelTVId { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public WheelTV()
        {
            this.IsMoveable = true;
        }

        public WheelTV(int wheelTVId)
        {
            this.WheelTVId = wheelTVId;
            this.IsMoveable = true;
        }
        public WheelTV(int wheelTVId, int width, int height)
        {
            this.WheelTVId = wheelTVId;
            this.Width = width;
            this.Height = height;
            this.IsMoveable = true;
        }
    }
}
