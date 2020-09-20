using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationSystem.Scheduler;
using System;
using ReservationSystem.Validation;
using ReservationSystem.Models;
using System.Collections.Generic;

namespace ReservationSystem.Test
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void ValidateOffice_WithCorrectData_ReturnTrue()
        {
            // Arrange
            Office office = new Office()
            {
                Country = "Germany",
                City = "Berlin",
                OfficeId = 1
            };

            // Act
            var result = Validator.ValidateOffice(office);

            // Assert
            Assert.AreEqual(result, true);
        }
        
        [TestMethod]
        public void ValidateOffice_WithIncorrectData_ReturnFalse()
        {
            // Arrange
            Office office = new Office()
            {
                Country = string.Empty,
                City = "Berlin",
                OfficeId = 1
            };

            // Act
            var result = Validator.ValidateOffice(office);

            // Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void ValidateRoom_WithIncorrectData_ReturnTrue()
        {
            // Arrange
            Room room = new Room()
            {
                RoomId = 1,
                OfficeId = 1,
                RoomName = "Berlin Room 1",
                Capacity = 10,
                HasChairs = false
            };

            // Act
            var result = Validator.ValidateRoom(room);

            // Assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ValidateRoom_WithIncorrectData_ReturnFalse()
        {
            // Arrange
            Room room = new Room()
            {
                RoomId = 1,
                OfficeId = 1,
                RoomName = "Berlin Room 1",
                Capacity = -2,
                HasChairs = false
            };

            // Act
            var result = Validator.ValidateRoom(room);

            // Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsRoomAvaliable_WithCorrectTimeSlot_ReturnTrue()
        {
            // Arrange
            List<Reservation> reservations = DataGenerator.GetValidReservations();
            int roomId = 1;
            DateTime from = new DateTime(2020, 9, 20, 11, 30, 0);
            DateTime to = new DateTime(2020, 9, 20, 12, 0, 0);


            // Act
            var result = Validator.IsRoomAvaliable(roomId,from,to,reservations);

            // Assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsRoomAvaliable_WithIncorrectTimeSlot_ReturnFalse()
        {
            // Arrange
            List<Reservation> reservations = DataGenerator.GetValidReservations();
            int roomId = 1;
            DateTime from = new DateTime(2020, 9, 20, 10, 0, 0);
            DateTime to = new DateTime(2020, 9, 20, 10, 30, 0);


            // Act
            var result = Validator.IsRoomAvaliable(roomId, from, to, reservations);

            // Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsRoomAvaliable_WithLargeTimeSlot_ReturnFalse()
        {
            // as room cannot be avalaible during all this period since a meeting between 10 and 11 is scheduled
            // Arrange
            List<Reservation> reservations = DataGenerator.GetValidReservations();
            int roomId = 1;
            DateTime from = new DateTime(2020, 9, 20, 1, 0, 0);
            DateTime to = new DateTime(2020, 9, 20, 12, 0, 0);


            // Act
            var result = Validator.IsRoomAvaliable(roomId, from, to, reservations);

            // Assert
            Assert.AreEqual(result, false);
        }
    }
}
