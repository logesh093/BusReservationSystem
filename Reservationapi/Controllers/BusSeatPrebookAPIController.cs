using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UserData.Core.IServices;
using UserData.Core.Model;

namespace BusReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusSeatPrebookAPIController : ControllerBase
    {
        private readonly IServices _services;
        public BusSeatPrebookAPIController(IServices services)
        {
            _services = services;
        }
        #region Login Detail

        [HttpPost]
        [Route("UserOrAdminLoginVerification")]
        public IActionResult UserOrAdminLoginVerification(UserLogin logindetail)
        {
            var logins = _services.UserAuthentication(logindetail);
            return Ok(logins);

        }
        #endregion

        #region SignUp Detail
        [HttpPost]
        [Route("InsertUserOrAdminDetails")]
        public IActionResult InsertUserOrAdminDetails(UserOrAdminSignUp signupDetail)
        {

            bool exist = _services.AddSignUpDetails(signupDetail);
            return Ok(exist);
        }
        #endregion

        #region Get Bus Dashboard
        [HttpGet]
        [Route("GetBusDashboard")]
        public IActionResult GetBusDashboard()
        {
            var getBusList = _services.GetBusDashboard();
            return Ok(getBusList);
        }
        #endregion

        #region Get Bus Detail By Id
        [HttpGet]
        [Route("GetBusDetailById")]
        public IActionResult GetBusDetailById(int busId)
        {

            var getBus = _services.GetBusDetailById(busId);
            return Ok(getBus);
        }
        #endregion

        #region Add or Update Bus Detail
        [HttpPost]
        [Route("AddOrUpdateBus")]
        public IActionResult AddOrUpdateBus(BusMaster busDetails)
        {

            bool isAdded = _services.AddOrUpdateBus(busDetails);
            if (isAdded == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        #endregion

        #region Get Bus Travel Schedule
        [HttpGet]
        [Route("GetBusTravelSchedule")]
        public IActionResult GetBusTravelSchedule(int busId)
        {
            var getBusTravelSchedule = _services.GetBusTravelSchedule(busId);
            return Ok(getBusTravelSchedule);
        }
        #endregion

        #region Get Bus Schedule By Travel Id
        [HttpGet]
        [Route("GetBusScheduleByTravelId")]
        public IActionResult GetBusScheduleByTravelId(int travelId)
        {
            var getBusScheduleById = _services.GetBusScheduleByTravelId(travelId);
            return Ok(getBusScheduleById);
        }
        #endregion
        #region Create Travel Id
        [HttpPost]
        [Route("CreateOrUpdateTravelId")]
        public IActionResult CreateOrUpdateTravelId(BusTravelScheduleModel busDetails)
        {

            bool isAdded = _services.CreateOrUpdateTravelId(busDetails);
            if (isAdded == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
        #endregion

        #region
        [HttpGet]
        [Route("UserDetailPage")]
        public IActionResult UserDetailPage(int UserId)
        {
            var UserDetailList = _services.UserDetailPage(UserId);
            return Ok(UserDetailList);
        }
        [HttpGet]
        [Route("UserProfile")]
        public IActionResult UserProfile(int UserId)
        {
            var getUserProfile = _services.UserProfile(UserId);
            return Ok(getUserProfile);
        }
        #endregion

        #region Search Available bus
        [HttpPost]
        [Route("SearchAvailableBuses")]
        public IActionResult SearchAvailableBuses(FindBus SearchDetails)
        {
            var BusList = _services.SearchAvailableBuses(SearchDetails);
            return Ok(BusList);
        }
        #endregion

        #region Get booked Seat number
        [HttpGet]
        [Route("GetBookedSeatno")]
        public IActionResult GetBookedSeatno(int travelId)
        {
            var SeatList = _services.GetBookedSeatno(travelId);
            return Ok(SeatList);
        }
        #endregion

        #region Insert passengers details
        [HttpPost]
        [Route("InsertPassengerDeatil")]
        public IActionResult InsertPassengerDeatil(PassengerDetails pasengerDetail)
        {
            bool IsInserted = _services.InsertPassengerDeatil(pasengerDetail);
            return Ok(IsInserted);
        }
        #endregion
        #region Insert Payment details
        [HttpPost]
        [Route("InsertPaymentDetail")]
        public IActionResult InsertPaymentDetail(PaymentModel paymentDetail)
        {
           bool IsInserted = _services.InsertPaymentDetail(paymentDetail);
            return Ok(IsInserted);
        }
        #endregion

        #region View passengers details
        [HttpPost]
        [Route("ViewPassengerDetail")]
        public IActionResult ViewPassengerDetail(PassengerDetails passengerDetails)
        {
            var passengerList = _services.ViewPassengerDetail(passengerDetails);
            return Ok(passengerList);
        }
        #endregion

        #region Get ticket list
        [HttpGet]
        [Route("GetTicketList")]
        public IActionResult GetTicketList(int travelId)
        {
            var TicketList = _services.GetTicketList(travelId);
            return Ok(TicketList);
        }
        #endregion

        #region Download ticket 
        [HttpGet]
        [Route("DownloadTicket")]
        public IActionResult DownloadTicket(int travelId, int userId)
        {
            var TicketList = _services.DownloadTicket(travelId,userId);
            return Ok(TicketList);
        }
        #endregion

        #region Get passenger for ticket download 
        [HttpGet]
        [Route("GetPassenger")]
        public IActionResult GetPassenger(int referenceId)
        {
            var TicketList = _services.GetPassenger(referenceId);
            return Ok(TicketList);
        }
        #endregion
        #region Delete Bus Travel
        [HttpGet]
        [Route("DeleteBusTravel")]
        public IActionResult DeleteBusTravel(int travelId)
        {
            var TicketList = _services.DeleteBusTravel(travelId);
            return Ok(TicketList);
        }
        #endregion

        #region Get Fare
        [HttpGet]
        [Route("GetFare")]
        public IActionResult GetFare(int travelId)
        {
            var fare = _services.GetFare(travelId);
            return Ok(fare);
        }
        #endregion

        #region Cancel Ticket
        [HttpGet]
        [Route("CancelTicket")]
        public IActionResult CancelTicket(int passengerId)
        {
            bool IsInserted = _services.CancelTicket(passengerId);
            return Ok(IsInserted);
        }
        #endregion

        #region Delete Bus Travel
        [HttpGet]
        [Route("GetReferenceId")]
        public IActionResult GetReferenceId(int travelId,int busId,int userId)
        {
            var TicketList = _services.GetReferenceId(travelId,busId,userId);
            return Ok(TicketList);
        }
        #endregion
    }


}
