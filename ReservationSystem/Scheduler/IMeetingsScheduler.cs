using ReservationSystem.Enum;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Scheduler
{
    public interface IMeetingsScheduler
    {
        List<Room> GetAllAvaliableRooms(int officeId, DateTime from, DateTime to);
        public string ReserveRoom(Reservation reservation, out List<string> errorMessage);

    }
}
