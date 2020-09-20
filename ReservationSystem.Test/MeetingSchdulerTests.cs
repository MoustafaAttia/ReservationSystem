using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationSystem.Scheduler;
using System;

namespace ReservationSystem.Test
{
    [TestClass]
    public class MeetingSchdulerTests
    {
        [TestMethod]
        public void GetAllAvaliableRooms_WithCorrectDate_ReturnAllAvaliableRooms_AtBerlinOffice()
        {
            // Arrange
            var mockService = MockSettings.GetService_With_Offices_Rooms_Reservations(DataGenerator.GetValidOffices(), 
                                                                                      DataGenerator.GetValidRoomsForOffice(), 
                                                                                      DataGenerator.GetValidReservations());
            var meetingScheduler = new MeetingsScheduler(mockService.Object);
            DateTime from = new DateTime(2020, 9, 20, 10, 0, 0);
            DateTime to = new DateTime(2020, 9, 20, 11, 0, 0);
            
            // Act
            var result = meetingScheduler.GetAllAvaliableRooms(1, from, to);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].RoomId, 2);
            Assert.AreEqual(result[1].RoomId, 3);
        }

        [TestMethod]
        public void GetAllAvaliableRooms_WithCorrectDate_ReturnAllAvaliableRooms_AtAmsterdamOffice()
        {
            // Arrange
            var mockService = MockSettings.GetService_With_Offices_Rooms_Reservations(DataGenerator.GetValidOffices(),
                                                                                      DataGenerator.GetValidRoomsForOffice(),
                                                                                      DataGenerator.GetValidReservations());
            var meetingScheduler = new MeetingsScheduler(mockService.Object);
            DateTime from = new DateTime(2020, 9, 20, 8, 0, 0);
            DateTime to = new DateTime(2020, 9, 20, 9, 0, 0);

            // Act
            var result = meetingScheduler.GetAllAvaliableRooms(2, from, to);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].RoomId, 5);
        }
    }
}
