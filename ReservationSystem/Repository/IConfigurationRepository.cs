using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository
{
    public interface IConfigurationRepository
    {
        List<T> GetAll<T>();
        void AddNew<T>(T item);
        void DeleteItem<T>(T item);
        void EditItem<T>(T item);


    }
}
