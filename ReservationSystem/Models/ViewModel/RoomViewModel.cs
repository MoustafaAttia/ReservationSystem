using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.ViewModel
{
    public class RoomViewModel
    {
        public List<Room> allRooms { get; set; }
        public Room room { get; set; }
        public SelectList offices { get; set; }
        public List<string> Errors { get; set; }
        public int officeId { get; set; }
    }
}
