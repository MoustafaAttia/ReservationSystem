using ReservationSystem.Models;
using ReservationSystem.Repository;
using ReservationSystem.Repository.Factory;
using ReservationSystem.Service;
using ReservationSystem.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Scheduler
{
    public class MeetingsScheduler : IMeetingsScheduler
    {
        private List<Office> allOffices;
        private List<Room> allRooms;
        private List<Reservation> allReservations;

        private readonly ConfigurationRepository _officeRepository, _roomRepository, _reservationRepository;
        private IService _service;
        public MeetingsScheduler(IService service)
        {
            this._service = service;
            
            this._officeRepository = RepositoryFactory.GetConfigurationRepository<Office>(this._service);
            this._roomRepository = RepositoryFactory.GetConfigurationRepository<Room>(this._service);
            this._reservationRepository = RepositoryFactory.GetConfigurationRepository<Reservation>(this._service);

            this.allRooms = this._roomRepository.GetAll<Room>();
            this.allOffices = this._officeRepository.GetAll<Office>();
            this.allReservations = this._reservationRepository.GetAll<Reservation>();

        }

        public List<Room> GetAllAvaliableRooms(int officeId, DateTime from, DateTime to)
        {
            if (!ValidateRomms())
            {
                return new List<Room>();
            }
            List<Room> result = new List<Room>(this.allRooms).Where(x => x.OfficeId == officeId).ToList();
            foreach (Room item in this.allRooms)
            {
                if (allReservations.Where(x => x.RoomId == item.RoomId && x.timeFrom == from && x.timeTo == to).FirstOrDefault() != null)
                {
                    result.Remove(item);
                }
            }
            
            return result;
        }

        public string ReserveRoom(Reservation reservation, out List<string> errorMessage)
        {
            errorMessage = new List<string>();
                       

            #region Validating properties
            if (!ValidateRomms())
            {
                errorMessage.Add("There is no rooms defined.");
                return "-1";
            }

            if (!ValidateRomms(reservation.RoomId))
            {
                errorMessage.Add("Selected room is invalid.");
                return "-1";
            }

            if (!Validator.IsReservationCorrect(reservation, this.allReservations, this.allRooms, out errorMessage))
            {
                return "-1";
            }
            #endregion

            try
            {
                reservation.ReservationId = GenerateReservationId(reservation.RoomId, reservation.timeFrom, reservation.timeTo);
                reservation.CreationDate = DateTime.Now;
                reservation.SetStartTime(reservation.timeFrom.Hour, reservation.timeFrom.Minute);
                reservation.SetEndTime(reservation.timeTo.Hour, reservation.timeTo.Minute);

                this._reservationRepository.AddNew<Reservation>(reservation);


                return reservation.ReservationId;
            }
            catch (Exception ex)
            {
                errorMessage.Add("Unexpected exception happen. " + ex.Message);
                return "-1";
            }

        }


        #region Private Methods
        

        private bool ValidateRomms()
        {
            if (this.allRooms == null || this.allRooms.Count == 0)
                return false;
            return true;
                
        }

        private bool ValidateRomms(int roomId)
        {
            if (this.allRooms.FirstOrDefault(x => x.RoomId == roomId) == null)
                return false;
            return true;
        }

        private string GenerateReservationId(int roomId, DateTime from, DateTime to)
        {
            // there should be a logic behind generating IDs, but for simplicty, this method will only concatenate roomId with start and end time slot and randomly number at the end
            Random random = new Random();
            string pattern = "{0}x{1}x{2}x{3}";
            string result;

            result = string.Format(pattern, roomId, from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"), random.Next(1, 1000));


            return result;
        }
        #endregion
    }
}
