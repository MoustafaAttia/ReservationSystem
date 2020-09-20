using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.ViewModel
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            this.Errors = new List<string>();
        }
        public Reservation reservation { get; set; }
        public List<Reservation> allReservations { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public List<Office> allOffices { get; set; }
        public List<Room> allRooms { get; set; }
        public SelectList offices { get; set; }
        public SelectList rooms { get; set; }
        public int SelectedFilterRoom { get; set; }
    }
}
