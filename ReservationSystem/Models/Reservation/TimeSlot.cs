using ReservationSystem.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class TimeSlot 
    {
        public DateTime From { get; private set;  }
        public DateTime To { get; private set; }
        private const string OutOfRangeMessage = "Parameter {0} passed is out of range !";

        public TimeSlot(int year, Month month, int day)
        {
            if (year < DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "year"));
            if (day <= 0 || day > 31)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "day"));

            this.From = new DateTime(year, (int)month, day, 0, 0, 0);
            this.To = new DateTime(year, (int)month, day, 0, 0, 0);
        }

        public void SetStartTime(int hour, int minutes)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "hour"));
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "minutes"));

            this.From.AddHours(hour);
            this.From.AddMinutes(RoundToFive(minutes));
        }

        public void SetEndTime(int hour, int minutes)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "hour"));
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(string.Format(OutOfRangeMessage, "minutes"));


            this.To.AddHours(hour);
            this.To.AddMinutes(RoundToFive(minutes));
            if (this.To <= this.From)
            {
                this.To = new DateTime(this.To.Year, this.To.Month, this.To.Day, 0, 0, 0);
                throw new InvalidDataException("Ending time is less than or equal to Starting time !");
            }
        }

        private int RoundToFive(int n)
        {
            return (n + 4) / 5 * 5;
        }
    }
}
