using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationSystem.Models;
using ReservationSystem.Models.ViewModel;
using ReservationSystem.Repository;
using ReservationSystem.Repository.Factory;
using ReservationSystem.Scheduler;
using ReservationSystem.Service;

namespace ReservationSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private ReservationViewModel reservationViewModel;
        private readonly IMeetingsScheduler _meetingsScheduler;
        private readonly ConfigurationRepository _officeRepository, _roomRepository, _reservationRepository;
        private readonly IService _service;

        private List<Office> allOffices;
        private List<Room> allRooms;
        private List<Reservation> allReservations;

        public ReservationsController(IService service, IMeetingsScheduler meetingsScheduler)
        {
            this._service = service;
            this._meetingsScheduler = meetingsScheduler;
            this._officeRepository = RepositoryFactory.GetConfigurationRepository<Office>(this._service);
            this._roomRepository = RepositoryFactory.GetConfigurationRepository<Room>(this._service);
            this._reservationRepository = RepositoryFactory.GetConfigurationRepository<Reservation>(this._service);

            allOffices = this._officeRepository.GetAll<Office>().OrderBy(x => x.OfficeId).ToList();
            allRooms = this._roomRepository.GetAll<Room>().OrderBy(x => x.RoomId).ToList();
            allReservations = this._reservationRepository.GetAll<Reservation>().OrderBy(x => x.CreationDate).ToList();

            reservationViewModel = new ReservationViewModel();
            reservationViewModel.allReservations = this.allReservations;
            reservationViewModel.allOffices = this.allOffices;
            reservationViewModel.allRooms = this.allRooms;

            reservationViewModel.offices = new SelectList(
                this.allOffices.Select(x => new { Id = x.OfficeId, Value = x.City }),
                "Id",
                "Value");

            reservationViewModel.rooms = new SelectList(
               this.allRooms.Select(x => new { Id = x.RoomId, Value = x.RoomName }),
               "Id",
               "Value");
        }
        public IActionResult Index()
        {
            return View("Views/Home/Index.cshtml");
        }

        public ActionResult ListReservation()
        {
            if (this.allOffices == null || this.allOffices.Count  == 0)
            {
                reservationViewModel.Errors.Add("No offices created, Please configure offices first.");
            }
            
            if (this.allRooms == null || this.allRooms.Count == 0)
            {
                reservationViewModel.Errors.Add("No rooms created, Please configure rooms first.");
            }
            
            reservationViewModel.reservation = new Reservation();
            var roomsFilterDDL = allRooms;
            roomsFilterDDL.Add(new Room() { RoomId = 0, RoomName = "Show all" });
            reservationViewModel.rooms = new SelectList(
               roomsFilterDDL.OrderBy(y => y.RoomId).Select(x => new { Id = x.RoomId, Value = x.RoomName }),
               "Id",
               "Value",
               0);
            this.allReservations = this._reservationRepository.GetAll<Reservation>().OrderBy(x => x.CreationDate).ToList();
            reservationViewModel.allReservations = this.allReservations;
            return View("Views/Reservation/ListReservations.cshtml", reservationViewModel);
        }

        public ActionResult AddReservation()
        {
            if (this.allOffices == null || this.allOffices.Count == 0)
            {
                reservationViewModel.Errors.Add("No offices created, Please configure offices first.");
            }

            if (this.allRooms == null || this.allRooms.Count == 0)
            {
                reservationViewModel.Errors.Add("No rooms created, Please configure rooms first.");
            }

            reservationViewModel.reservation = new Reservation();
            return View("Views/Reservation/CreateReservation.cshtml", reservationViewModel);
        }

        public ActionResult DeleteReservation(string id)
        {
            var res = allReservations.Where(x => x.ReservationId == id).FirstOrDefault();
            string errorMessage = string.Empty;
            this._reservationRepository.DeleteItem<Reservation>(res, out errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Message = errorMessage;
                return this.ListReservation();
            }
            else
                return this.ListReservation();
        }
        

        public ActionResult CreateReservation(Reservation reservation)
        {
            List<string> errors = new List<string>();
            string result =  this._meetingsScheduler.ReserveRoom(reservation, out errors);
            if (result == "-1" || errors.Count > 0)
            {
                this.reservationViewModel.reservation = reservation;
                this.reservationViewModel.Errors = errors;
                return View("Views/Reservation/CreateReservation.cshtml", reservationViewModel);
            }
            return View("Views/Home/Index.cshtml");
        }

        [HttpGet]
        public JsonResult GetRoomByOfficeId(int officeId)
        {
            List<Room> rooms = new List<Room>();
            rooms = this.allRooms.Where(m => m.OfficeId == officeId).ToList();
            SelectList roomsSelectedList = new SelectList(rooms.Select(x => new { Id = x.RoomId, Value = x.RoomName, haschairs = x.HasChairs }) , "Id", "Value", "haschairs");
            return Json(roomsSelectedList.Items);
        }
        
    }
}