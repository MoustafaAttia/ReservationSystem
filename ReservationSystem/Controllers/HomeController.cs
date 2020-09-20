using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Logging;
using ReservationSystem.Models;
using ReservationSystem.Models.ViewModel;
using ReservationSystem.Repository;
using ReservationSystem.Repository.Factory;
using ReservationSystem.Service;

namespace ReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConfigurationRepository _officeRepository, _roomRepository;
        private IService _service;
        private OfficeViewModel OfficeViewModel;
        private RoomViewModel roomViewModel;
        private List<Office> allOffices;
        private List<Room> allRooms;

        public HomeController(ILogger<HomeController> logger, IService service)
        {
            _logger = logger;
            this._service = service;
            this._officeRepository = RepositoryFactory.GetConfigurationRepository<Office>(this._service);
            this._roomRepository = RepositoryFactory.GetConfigurationRepository<Room>(this._service);
            
            allOffices = this._officeRepository.GetAll<Office>().OrderBy(x => x.OfficeId).ToList();
            allRooms = this._roomRepository.GetAll<Room>().OrderBy(x => x.RoomId).ToList();
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Offices()
        {
            OfficeViewModel = new OfficeViewModel();
            allOffices = this._officeRepository.GetAll<Office>().OrderBy(x => x.OfficeId).ToList();
            OfficeViewModel.allOffices = this.allOffices;
           
            return View("Views/Office/CreateOffice.cshtml", OfficeViewModel);
        }

        public ActionResult Rooms()
        {
            roomViewModel = new RoomViewModel();
            var offices = allOffices.Select(x => new { Id = x.OfficeId, Value = x.City });
            roomViewModel.offices = new SelectList(offices, "Id", "Value");
            allRooms = this._roomRepository.GetAll<Room>().OrderBy(x => x.RoomId).ToList();
            roomViewModel.allRooms = this.allRooms;
            if (allOffices == null || allOffices.Count == 0)
            {
                roomViewModel.Errors.Add("No offices created to add rooms belongs to it, Please add at least one office location.");
            }
            return View("Views/Room/CreateRoom.cshtml", roomViewModel);
        }

        public ActionResult CreateOffice(Office office)
        {
            if (office.OfficeId != 0)
            {
                this._officeRepository.EditItem<Office>(office);
                return this.Offices();
            }
            else
            {
                this._officeRepository.AddNew<Office>(office);
                return this.Offices();
            }
        }

        public ActionResult EditOffice(int id)
        {
            var office = allOffices.Where(x => x.OfficeId == id).FirstOrDefault();
            OfficeViewModel = new OfficeViewModel();
            OfficeViewModel.allOffices = this.allOffices;
            OfficeViewModel.office = office;
            return View("Views/Office/CreateOffice.cshtml", OfficeViewModel);
        }

        public ActionResult EditRoom(int id)
        {
            var room = allRooms.Where(x => x.RoomId == id).FirstOrDefault();
            roomViewModel = new RoomViewModel();
            roomViewModel.allRooms = this.allRooms;
            var offices = allOffices.Select(x => new { Id = x.OfficeId, Value = x.City });
            roomViewModel.offices = new SelectList(offices, "Id", "Value");
            roomViewModel.room = room;
            return View("Views/Room/CreateRoom.cshtml", roomViewModel);
        }

        public ActionResult DeleteOffice(int id)
        {
            var office = allOffices.Where(x => x.OfficeId == id).FirstOrDefault();
            string errorMessage;
            this._officeRepository.DeleteItem<Office>(office, out errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Message = errorMessage;
                return this.Offices();
            }
            else
                return this.Offices();
        }

        public ActionResult DeleteRoom(int id)
        {
            var room = allRooms.Where(x => x.RoomId == id).FirstOrDefault();
            string errorMessage = string.Empty;
            this._roomRepository.DeleteItem<Room>(room, out errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Message = errorMessage;
                return this.Rooms();
            }
            else
                return this.Rooms();
        }

        public ActionResult CreateRoom(Room room)
        {
            if (room.RoomId != 0)
            {
                this._roomRepository.EditItem<Room>(room);
                return this.Rooms();
            }
            else
            {
                this._roomRepository.AddNew<Room>(room);
                return this.Rooms();
            }
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
