using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Scheduler;
using ReservationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository
{
    public class ReservationRepository : ConfigurationRepository
    {
        private IService _service;
        public ReservationRepository(IService service) : base(service)
        {
            _service = service;
        }

        public override void AddNew<T>(T item)
        {
            List<Reservation> reservations = GetAll<Reservation>();
            Reservation res = item as Reservation;

            reservations.Add(res);
            string jsonContent = JsonConvert.SerializeObject(reservations);
            this._service.WriteFile<Reservation>(jsonContent);
        }

        public override void DeleteItem<T>(T item, out string errorMessage)
        {
            List<Reservation> reservations = GetAll<Reservation>();
            Reservation res = item as Reservation;
            res = reservations.Where(x => x.ReservationId == res.ReservationId).FirstOrDefault();

            reservations.Remove(res);
            this._service.WriteFile<Reservation>(string.Empty);
            string jsonContent = JsonConvert.SerializeObject(reservations);
            this._service.WriteFile<Reservation>(jsonContent);
            errorMessage = string.Empty;
        }

        public override void EditItem<T>(T item)
        {
            List<Reservation> reservations = GetAll<Reservation>();
            Reservation res = item as Reservation;
            Reservation oldRes = reservations.Where(x => x.ReservationId == res.ReservationId).FirstOrDefault();
            reservations.Remove(oldRes);
            this._service.WriteFile<Reservation>(string.Empty);
            reservations.Add(res);
            string jsonContent = JsonConvert.SerializeObject(reservations);
            this._service.WriteFile<Reservation>(jsonContent);
        }

        public override List<T> GetAll<T>()
        {
            List<Reservation> reservations = new List<Reservation>();

            string jsonResult = this._service.ReadFile<Reservation>();
            if (string.IsNullOrEmpty(jsonResult))
            {
                return reservations as List<T>;
            }
            reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonResult);

            return reservations as List<T>;
        }
    }
}
