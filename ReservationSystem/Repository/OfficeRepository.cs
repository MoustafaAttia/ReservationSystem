using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Repository.Factory;
using ReservationSystem.Service;
using ReservationSystem.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository
{
    public class OfficeRepository : ConfigurationRepository
    {
        private IService _service;
        public OfficeRepository(IService service) : base(service)
        {
            _service = service;
        }

        public override void AddNew<T>(T item)
        {
            List<Office> offices = GetAll<Office>();
            Office office = item as Office;
            office.OfficeId = offices.Count == 0 ? 1 : offices.Max(x => x.OfficeId) + 1;
            offices.Add(office);
            string jsonContent = JsonConvert.SerializeObject(offices);
            this._service.WriteFile<Office>(jsonContent);
        }

        public override void DeleteItem<T>(T item, out string errorMessage)
        {
            List<Office> offices = GetAll<Office>();
            Office office = item as Office;
            office = offices.Where(x => x.OfficeId == office.OfficeId).FirstOrDefault();
            if (Validator.IsAssociatedWithRooms(office.OfficeId, this._service))
            {
                errorMessage = "Cannot delete this office as it is associated with other rooms, Please delete rooms associated with it first.";
                return;
            }
            offices.Remove(office);
            this._service.WriteFile<Office>(string.Empty);
            string jsonContent = JsonConvert.SerializeObject(offices);
            this._service.WriteFile<Office>(jsonContent);
            errorMessage = string.Empty;
        }
        public override void EditItem<T>(T item)
        {
            List<Office> offices = GetAll<Office>();
            Office office = item as Office;
            Office oldOffice = offices.Where(x => x.OfficeId == office.OfficeId).FirstOrDefault();
            offices.Remove(oldOffice);
            this._service.WriteFile<Office>(string.Empty);
            offices.Add(office);
            string jsonContent = JsonConvert.SerializeObject(offices);
            this._service.WriteFile<Office>(jsonContent);
        }

        public override List<T> GetAll<T>()
        {
            List<Office> offices = new List<Office>();
            
            string jsonResult = this._service.ReadFile<Office>();
            if (string.IsNullOrEmpty(jsonResult))
            {
                return offices as List<T>;
            }
            offices = JsonConvert.DeserializeObject<List<Office>>(jsonResult);

            return offices as List<T>;
        }

       
    }
}
