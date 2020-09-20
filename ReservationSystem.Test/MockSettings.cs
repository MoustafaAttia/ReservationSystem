using Moq;
using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationSystem.Test
{
    public static class MockSettings
    {
        public static Mock<IService> GetService_With_Offices_Rooms_Reservations(List<Office> offices, List<Room> rooms, List<Reservation> reservations)
        {
            var serviceMock = new Mock<IService>();
            serviceMock.Setup(p => p.WriteFile<It.IsAnyType>(It.IsAny<string>()));

            string jsonContentOffice = JsonConvert.SerializeObject(offices);
            serviceMock.Setup(p => p.ReadFile<Office>()).Returns(jsonContentOffice);

            string jsonContentRooms = JsonConvert.SerializeObject(rooms);
            serviceMock.Setup(p => p.ReadFile<Room>()).Returns(jsonContentRooms);

            string jsonContentReservations = JsonConvert.SerializeObject(reservations);
            serviceMock.Setup(p => p.ReadFile<Reservation>()).Returns(jsonContentReservations);


            return serviceMock;
        }
    }
}
