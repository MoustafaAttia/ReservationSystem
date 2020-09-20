using System.Collections.Generic;

namespace ReservationSystem.Service
{
    public interface IService
    {
        void WriteFile<T>(string content);
        string ReadFile<T>();
    }
}
