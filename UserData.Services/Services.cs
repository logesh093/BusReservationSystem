using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Core.IRepository;
using UserData.Core.IServices;
using UserData.Core.Model;

namespace UserData.Services
{
    public class Services : IServices
    {
        private readonly IRepository _repository;
        public Services(IRepository repository)
        {
            _repository = repository;
        }
        public UserLogin UserAuthentication(UserLogin userLogin)
        {
            return _repository.UserAuthentication(userLogin);
        }
        public bool AddSignUpDetails(UserOrAdminSignUp signupDetail)
        {
            return _repository.AddSignUpDetails(signupDetail);
        }
        public List<BusMaster> GetBusDashboard()
        {
            return _repository.GetBusDashboard();
        }
        public BusMaster GetBusDetailById(int busId)
        {
            return _repository.GetBusDetailById(busId);
        }
        public bool AddOrUpdateBus(BusMaster busDetails)
        {
            return _repository.AddOrUpdateBus(busDetails);
        }
        public List<BusTravelScheduleModel> GetBusTravelSchedule(int busId)
        {
            return _repository.GetBusTravelSchedule(busId);
        }
        public BusTravelScheduleModel GetBusScheduleByTravelId(int travelId)
        {
            return _repository.GetBusScheduleByTravelId(travelId);
        }
        public bool CreateOrUpdateTravelId(BusTravelScheduleModel busDetails)
        {
            return _repository.CreateOrUpdateTravelId(busDetails);
        }
        public List<UserDetailList> UserDetailPage(int UserId)
        {
            return _repository.UserDetailPage(UserId);
        }
        public UserPage UserProfile(int UserId)
        {
            return _repository.UserProfile(UserId);
        }
        public List<BusTravelScheduleModel> SearchAvailableBuses(FindBus SearchDetails)
        {
            return _repository.SearchAvailableBuses(SearchDetails);
        }
        public List<BookTicket> GetBookedSeatno(int travelId)
        {
            return _repository.GetBookedSeatno(travelId);
        }
        public bool InsertPassengerDeatil(PassengerDetails passengerDetails)
        {
            return _repository.InsertPassengerDeatil(passengerDetails);
        }
        public bool InsertPaymentDetail(PaymentModel paymentDetail)
        {
            return _repository.InsertPaymentDetail(paymentDetail);
        }
        public List<PassengerDetails> ViewPassengerDetail(PassengerDetails passengerDetails)
        {
            return _repository.ViewPassengerDetail(passengerDetails);
        }
        public bool CancelTicket(int passengerId)
        {
            return _repository.CancelTicket(passengerId);
        }
        public List<PassengerDetails> GetTicketList(int travelId)
        {
            return _repository.GetTicketList(travelId);
        }
        public bool DeleteBusTravel(int travelId)
        {
            return _repository.DeleteBusTravel(travelId);
        }
        public FindBus GetReferenceId(int travelId, int busId, int userId)
        {
            return _repository.GetReferenceId(travelId, busId, userId);
        }
        public PaymentModel GetFare(int travelId)
        {
            return _repository.GetFare(travelId);
        }
        public DownloadTicket DownloadTicket(int travelId, int userId)
        {
            return _repository.DownloadTicket(travelId, userId);
        }

        public List<PassengerList> GetPassenger(int referenceId)
        {
            return _repository.GetPassenger(referenceId);
        }
    }
}
