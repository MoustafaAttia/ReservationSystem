using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Reservation
    {
        private const string OutOfRangeMessage = "Parameter {0} passed is out of range !";

        public Reservation()
        {
            int year = DateTime.Now.Year, month = DateTime.Now.Month, day = DateTime.Now.Day;
            if (year < DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "year"));
            if (month <= 0 || month > 12)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "day"));
            if (day <= 0 || day > 31)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "day"));

            this.timeFrom = new DateTime(year, (int)month, day, 0, 0, 0);
            this.timeTo = new DateTime(year, (int)month, day, 0, 0, 0);

            this.resources = new List<RoomResources>();
        }

        public void SetStartTime(int hour, int minutes)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "hour"));
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "minutes"));

            this.timeFrom = new DateTime(timeFrom.Year, timeFrom.Month, timeFrom.Day, 0, 0, 0);
            this.timeFrom = this.timeFrom.AddHours(hour);
            this.timeFrom = this.timeFrom.AddMinutes(RoundToFive(minutes));
        }

        public void SetEndTime(int hour, int minutes)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "hour"));
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "minutes"));

            this.timeTo = new DateTime(timeTo.Year, timeTo.Month, timeTo.Day, 0, 0, 0);
            this.timeTo = this.timeTo.AddHours(hour);
            this.timeTo = this.timeTo.AddMinutes(RoundToFive(minutes));
            if (this.timeTo <= this.timeFrom)
            {
                this.timeTo = new DateTime(this.timeTo.Year, this.timeTo.Month, this.timeTo.Day, 0, 0, 0);
                throw new InvalidDataException("Ending time is less than or equal to Starting time !");
            }
        }

        private int RoundToFive(int n)
        {
            return (n + 4) / 5 * 5;
        }

        [DisplayName("Reservation ID")]
        public string ReservationId { get; set; }
        [DisplayName("Start time")]
        public DateTime timeFrom { get; set; }
        [DisplayName("End time")]
        public DateTime timeTo { get; set; }
        [DisplayName("Meeting organizer")]
        public string Organizer { get; set; }
        [DisplayName("Room")]
        public int RoomId { get; set; }
        [DisplayName("Office")]
        public int OfficeId { get; set; }
        public List<RoomResources> resources { get; set; }
        [DisplayName("Number of attendees")]
        public int NumberOfAttendees { get; set; }
        [DisplayName("Meeting creation date")]
        public DateTime CreationDate { get; set; }
    }
}
