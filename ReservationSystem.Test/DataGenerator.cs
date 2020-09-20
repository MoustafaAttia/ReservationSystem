using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationSystem.Test
{
    public static class DataGenerator
    {
        public static List<Reservation> GetValidReservations()
        {
            return new List<Reservation>()
            {
                new Reservation()
                {
                    OfficeId = 1,
                    RoomId = 1,
                    CreationDate = DateTime.Now,
                    NumberOfAttendees = 9,
                    Organizer = "John",
                    timeFrom = new DateTime(2020,9,20,10,0,0),
                    timeTo = new DateTime(2020,9,20,11,0,0)
                },
                new Reservation()
                {
                    OfficeId = 2,
                    RoomId = 4,
                    CreationDate = DateTime.Now,
                    NumberOfAttendees = 9,
                    Organizer = "Alex",
                    timeFrom = new DateTime(2020,9,20,8,0,0),
                    timeTo = new DateTime(2020,9,20,9,0,0)
                }
            };
        }
        public static List<Office> GetValidOffices()
        {
            return new List<Office>()
            {
                new Office()
                {
                    Country = "Germany",
                    City = "Berlin",
                    OfficeId = 1
                },
                new Office()
                {
                    Country = "Amsterdam",
                    City = "Netherlands",
                    OfficeId = 2
                }
            };
        }
        public static List<Room> GetValidRoomsForOffice()
        {
            return new List<Room>()
            {
                new Room()
                {
                    RoomId = 1,
                    OfficeId = 1,
                    RoomName = "Berlin Room 1",
                    Capacity = 10,
                    HasChairs = false
                },
                new Room()
                {
                    RoomId = 2,
                    OfficeId = 1,
                    RoomName = "Berlin Room 2",
                    Capacity = 20,
                    HasChairs = true
                },
                new Room()
                {
                    RoomId = 3,
                    OfficeId = 1,
                    RoomName = "Berlin Room 3",
                    Capacity = 20,
                    HasChairs = true
                },
                new Room()
                {
                    RoomId = 4,
                    OfficeId = 2,
                    RoomName = "Netherlands Room 1",
                    Capacity = 50,
                    HasChairs = false
                },
                new Room()
                {
                    RoomId = 5,
                    OfficeId = 2,
                    RoomName = "Netherlands Room 2",
                    Capacity = 50,
                    HasChairs = false
                }
            };
        }
    }
}
