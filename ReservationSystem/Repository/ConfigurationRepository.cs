using ReservationSystem.Models;
using ReservationSystem.Service;
using ReservationSystem.Validation;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Repository
{
    public abstract class ConfigurationRepository
    {
        private readonly IService _service;

        public ConfigurationRepository(IService service)
        {
            this._service = service;
        }

        public abstract List<T> GetAll<T>();

        public abstract void AddNew<T>(T item);
        
        public abstract void DeleteItem<T>(T item, out string errorMessage);

        public abstract void EditItem<T>(T item);
        
    }
}
