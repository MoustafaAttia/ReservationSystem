using Newtonsoft.Json;
using ReservationSystem.Models;
using ReservationSystem.Service;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Repository
{
    public class RoomRepository : ConfigurationRepository
    {
        private IService _service;
        public RoomRepository(IService service) : base(service)
        {
            _service = service;
        }

        public override void AddNew<T>(T item)
        {
            List<Room> rooms = GetAll<Room>();
            Room room = item as Room;
            room.RoomId = rooms.Count == 0 ? 1 : rooms.Max(x => x.RoomId) + 1;
            rooms.Add(room);
            string jsonContent = JsonConvert.SerializeObject(rooms);
            this._service.WriteFile<Room>(jsonContent);
        }

        public override void DeleteItem<T>(T item, out string errorMessage)
        {
            List<Room> rooms = GetAll<Room>();
            Room room = item as Room;
            room = rooms.Where(x => x.RoomId == room.RoomId).FirstOrDefault();
            rooms.Remove(room);
            this._service.WriteFile<Room>(string.Empty);
            string jsonContent = JsonConvert.SerializeObject(rooms);
            this._service.WriteFile<Room>(jsonContent);
            errorMessage = string.Empty;
        }

        public override void EditItem<T>(T item)
        {
            List<Room> rooms = GetAll<Room>();
            Room room = item as Room;
            Room oldRoom = rooms.Where(x => x.RoomId == room.RoomId).FirstOrDefault();
            rooms.Remove(oldRoom);
            this._service.WriteFile<Room>(string.Empty);
            rooms.Add(room);
            string jsonContent = JsonConvert.SerializeObject(rooms);
            this._service.WriteFile<Room>(jsonContent);
        }

        public override List<T> GetAll<T>()
        {
            List<Room> rooms = new List<Room>();

            string jsonResult = this._service.ReadFile<Room>();
            if (string.IsNullOrEmpty(jsonResult))
            {
                return rooms as List<T>;
            }
            rooms = JsonConvert.DeserializeObject<List<Room>>(jsonResult);

            return rooms as List<T>;
        }
    }
}
