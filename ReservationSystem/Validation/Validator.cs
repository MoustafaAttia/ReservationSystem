using ReservationSystem.Models;
using ReservationSystem.Repository;
using ReservationSystem.Repository.Factory;
using ReservationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Validation
{
    public static class Validator
    {
        public static bool ValidateOffice(Office item)
        {
            if (item == null)
                return false;
            if (string.IsNullOrEmpty(item.City))
                return false;
            if (string.IsNullOrEmpty(item.Country))
                return false;

            return true;
        }

        public static bool ValidateRoom(Room item)
        {
            if (item == null)
                return false;
            if (string.IsNullOrEmpty(item.RoomName))
                return false;
            if (item.Capacity <= 0)
                return false;
            if (item.OfficeId <= 0)
                return false;

            return true;
        }

        public static bool IsReservationCorrect(Reservation reservation, List<Reservation> allReservations, List<Room> rooms, out List<string> errors)
        {
            bool result = true;
            errors = new List<string>();

            if (!Validator.IsRoomAvaliable(reservation.RoomId, reservation.timeFrom, reservation.timeTo, allReservations))
            {
                errors.Add("The room is not avaliable at this period of time.");
                result = false;
            }

            if (reservation.timeFrom == null || reservation.timeTo == null || reservation.timeTo <= reservation.timeFrom)
            {
                errors.Add("Time slot is empty or passed with invalid dates.");
                result = false;
            }
            int max = rooms.Where(x => x.RoomId == reservation.RoomId).FirstOrDefault().Capacity;

            if (reservation.NumberOfAttendees <= 0 || reservation.NumberOfAttendees > max)
            {
                errors.Add(string.Format("Number of attendes is invalid, or exceeding the room capacity which is {0} person.", max));
                result = false;
            }

            return result;
        }

        public static bool IsRoomAvaliable(int roomId, DateTime from, DateTime to, List<Reservation> allReservations)
        {
            if (allReservations == null || allReservations.Count == 0)
            {
                // no reservations are done yet so all rooms avaliable
                return true;
            }

            foreach (var res in allReservations)
            {
                if (res.RoomId == roomId && IsTimeIntersect(res.timeFrom, res.timeTo, from, to))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsTimeIntersect(DateTime firstFrom, DateTime firstTo, DateTime secondFrom, DateTime secondTo)
        {
            // -------- 1st ---------- 1en -----------
            // -------- 2st ---------- 2en ----------
            if (firstFrom == secondFrom && secondTo == firstTo)
            {
                return true;
            }


            // -------- 1st ----- 1en -----------
            // -------- 2st ---------- 2en ------
            if (firstFrom == secondFrom && secondTo > firstTo)
            {
                return true;
            }

            // -------- 1st ------------------ 1en -----------
            // -------- 2st ---------- 2en -------------------
            if (firstFrom == secondFrom && secondTo < firstTo)
            {
                return true;
            }

            // ---- 1st -------------- 1en ------
            // -------- 2st ---------- 2en ------
            if (firstFrom < secondFrom && secondTo == firstTo)
            {
                return true;
            }

            // -------- 1st -------------- 1en ------
            // --- 2st ------------------- 2en ------
            if (firstFrom > secondFrom && secondTo == firstTo)
            {
                return true;
            }

            // -------- 1st -------------- 1en ------------
            // --- 2st ------------------------- 2en ------
            if (firstFrom > secondFrom && secondTo > firstTo)
            {
                return true;
            }

            // -------- 1st ------------------- 1en ------------
            // -------------- 2st ------ 2en -------------------
            if (firstFrom < secondFrom && secondTo < firstTo)
            {
                return true;
            }
                
            return false;
        }

        public static bool IsAssociatedWithRooms(int officeId, IService service)
        {
            ConfigurationRepository roomRepository = RepositoryFactory.GetConfigurationRepository<Room>(service);
            var allRooms = roomRepository.GetAll<Room>();
            foreach (Room room in allRooms)
            {
                if (room.OfficeId == officeId)
                    return true;
            }
            return false;
        }
    }
}
