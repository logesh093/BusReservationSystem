using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Core.Model;

namespace UserData.Core.IServices
{
    public interface IServices
    {
        public bool AddSignUpDetails(UserOrAdminSignUp signupDetail);
        public UserLogin UserAuthentication(UserLogin userLogin);
        public List<BusMaster> GetBusDashboard();
        public BusMaster GetBusDetailById(int busId);
        public bool AddOrUpdateBus(BusMaster busDetails);
        public List<BusTravelScheduleModel> GetBusTravelSchedule(int busId);
        public BusTravelScheduleModel GetBusScheduleByTravelId(int travelId);
        public bool CreateOrUpdateTravelId(BusTravelScheduleModel busDetails);

        public List<UserDetailList> UserDetailPage(int UserId);
        public UserPage UserProfile(int UserId);
        public List<BusTravelScheduleModel> SearchAvailableBuses(FindBus SearchDetails);
        public List<BookTicket> GetBookedSeatno(int travelId);
        public bool InsertPassengerDeatil(PassengerDetails passengerDetails);
        public bool InsertPaymentDetail(PaymentModel paymentDetail);

        public List<PassengerDetails> ViewPassengerDetail(PassengerDetails passengerDetails);
        public bool CancelTicket(int passengerId);
        public List<PassengerDetails> GetTicketList(int travelId);
        public bool DeleteBusTravel(int travelId);
        public PaymentModel GetFare(int travelId);
        public FindBus GetReferenceId(int travelId, int busId, int userId);
        public DownloadTicket DownloadTicket(int travelId, int userId);
        public List<PassengerList> GetPassenger(int referenceId);
        public BusMaster GetBusName(int travelId);
    }
}
