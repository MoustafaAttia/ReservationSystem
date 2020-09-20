using ReservationSystem.Models;
using ReservationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository.Factory
{
    public static class RepositoryFactory
    {
        public static ConfigurationRepository GetConfigurationRepository<T>(IService service)
        {
            if (typeof(T) == typeof(Office))
            {
                return new OfficeRepository(service);
            }
            else if (typeof(T) == typeof(Room))
            {
                return new RoomRepository(service);
            }
            else if (typeof(T) == typeof(Reservation))
            {
                return new ReservationRepository(service);
            }
            else
                return null;
        }
    }
}
