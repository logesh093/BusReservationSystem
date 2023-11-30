using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Core.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using UserData.Core.Model;
using Dapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Documents;

namespace UserData.Repository
{
    public class Repository:IRepository
    {
        private readonly IConfiguration _configure;
        public IDbConnection Connection => new SqlConnection(_configure.GetConnectionString("DefaultConnection"));
        public Repository(IConfiguration configure)
        {
            _configure = configure;
        }

        public UserLogin UserAuthentication(UserLogin userLogin)
        {
            if (userLogin != null)
            {
                UserLogin loginDatga = new UserLogin();
                using (var connections = Connection)
                {
                    UserPassword passworddetail = connections.Query<UserPassword>(DapperSql.LoginDetail, new { @emailid = userLogin.UserEmailid }).SingleOrDefault();
                    if (passworddetail != null)
                    {
                        bool result = VerifyPassword(userLogin.UserPassword, passworddetail.HashPassword, passworddetail.SaltPassword);
                        if (result)
                        {
                            var userDetail = connections.Query<UserLogin>(DapperSql.GetUserDetail, new { @emailid = userLogin.UserEmailid }).SingleOrDefault();

                            return userDetail;
                        }
                        else
                        {
                            return loginDatga;
                        }


                    }

                }

            }
            return null;
        }
        #region Addsignup details
        public bool AddSignUpDetails(UserOrAdminSignUp signupDetail)
        {
            UserPassword obj = new UserPassword();
            if (signupDetail != null)
            {
                using (var connections = Connection)
                {
                    var signupdetail = connections.Query<UserOrAdminSignUp>(DapperSql.CheckDetail, new
                    {
                        @emailid = signupDetail.EmailId

                    }).SingleOrDefault();

                    if (signupdetail == null)
                    {
                        const int keySize = 64;
                        const int iterations = 350000;
                        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

                        obj.SaltPassword = RandomNumberGenerator.GetBytes(keySize);

                        var hash = Rfc2898DeriveBytes.Pbkdf2(
                            Encoding.UTF8.GetBytes(signupDetail.Password),
                           obj.SaltPassword,
                            iterations,
                            hashAlgorithm,
                            keySize);

                        obj.HashPassword = Convert.ToHexString(hash);

                        connections.Execute(DapperSql.InsertDetail,
                      new
                      {
                          @name = signupDetail.Name,
                          @gender=signupDetail.Gender,
                          @dob= signupDetail.DOB,
                          @address= signupDetail.Address,
                          @phonenumber = signupDetail.Phonenumber,
                          @email = signupDetail.EmailId,
                          @hash = obj.HashPassword,
                          @salt = obj.SaltPassword,
                          @admin= signupDetail.Is_Admin,
                      });
                        return true;
                    }


                }


            }


            return false;
        }
        #endregion

        #region Get Bus Dashboard
        public List<BusMaster> GetBusDashboard()
        {
            using (var connections = Connection)
            {
                var getBuslist = (connections.Query<BusMaster>(DapperSql.DashBoardData)).ToList();

                return getBuslist;
            }
        }
        #endregion

        #region Get Bus Detail By Id
        public BusMaster GetBusDetailById(int busId)
        {
            BusMaster BusDetail=new BusMaster();
            using (var connections = Connection)
            {
                var BusData = (connections.Query<BusMaster>(DapperSql.GetDataById,
                new
                {
                    @id = busId,
                })).SingleOrDefault();
                if (BusData == null)
                {
                    return BusDetail;
                }
                return BusData;
            }
        }
        #endregion

        #region
        public bool AddOrUpdateBus(BusMaster busDetails)
        {
            if (busDetails.BusId > 0)
            {

                using (var connections = Connection)
                {
                    var a = connections.Execute(DapperSql.BusDetailUpdate,
                    new
                    {
                        @id= busDetails.BusId,
                        @name=busDetails.BusName,
                        @windowseats=busDetails.WindowSeats,
                        @nonwindow=busDetails.NonWindowSeats,
                        @updatedtimestamp = DateTime.Now
                    });
                }

                return false;

            }
            else
            {
                using (var connections = Connection)
                {
                    var a = connections.Execute(DapperSql.InsertBus,
                    new
                    {
                        @id = busDetails.BusId,
                        @name = busDetails.BusName,
                        @windowseats = busDetails.WindowSeats,
                        @nonwindow = busDetails.NonWindowSeats,
                        @createdtimestamp = DateTime.Now,
                        @updatedtimestamp = DateTime.Now
                    });
                }
                return true;
            }
        }
        #endregion

        #region Get Bus Schedule By busId
        public List<BusTravelScheduleModel> GetBusTravelSchedule(int busId)
        {
            using (var connections = Connection)
            {
                var getBusSchedule = (connections.Query<BusTravelScheduleModel>(DapperSql.GetTravelScheduleData, new
                {
                    @busid=busId,
                })).ToList();

                return getBusSchedule;
            }
        }
        #endregion

        #region Get Schedule By Travel Id
        public BusTravelScheduleModel GetBusScheduleByTravelId(int travelId)
        {
            using (var connections = Connection)
            {
                var getScheduleById = (connections.Query<BusTravelScheduleModel>(DapperSql.GetScheduleByTravelId, new
                {
                    @travelid= travelId,
                })).SingleOrDefault();
                return getScheduleById;
            }
        }
        #endregion


        #region Create Travel by bus id
        public bool CreateTravelId(BusTravelScheduleModel busDetails)
        {
            bool Iscreated = false;
            using (var connections = Connection)
            {
                if(busDetails.TravelId>0)
                {
                    var getScheduleById = (connections.Query<BusTravelScheduleModel>(DapperSql.UpdateTravelId, new
                    {
                        @travelid = busDetails.TravelId,
                        @busid = busDetails.BusId,
                        @source = busDetails.Source,
                        @destination = busDetails.Destination,
                        @fare = busDetails.Fare,
                        @depaturetime = busDetails.DepatureDateTime,
                        @updatetimestamp = DateTime.Now,
                    }));
                    Iscreated = true;
                }
                else
                {
                    var getScheduleById = (connections.Query<BusTravelScheduleModel>(DapperSql.CreateTravelId, new
                    {
                        
                        @busid= busDetails.BusId,
                        @source= busDetails.Source,
                        @destination= busDetails.Destination,
                        @fare= busDetails.Fare,
                        @depaturetime=busDetails.DepatureDateTime,
                        @updatetimestamp = DateTime.Now,
                        @createtimestamp = DateTime.Now
                    }));
                    Iscreated = true;
                }
                

                return Iscreated;
            }
        }
        #endregion

        #region User Page
        public List<UserDetailList> UserDetailPage(int UserId)
        {
            
            using (var connections = Connection)
            {
                var UserData = (connections.Query<UserDetailList>(DapperSql.UserPage,
                new
                {
                    @userid = UserId,
                })).ToList();
                if (UserData == null)
                {
                    return UserData;
                }
                return UserData;
            }
        }
        public UserPage UserProfile(int UserId)
        {
            using (var connections = Connection)
            {
                var GetUserProfile=(connections.Query<UserPage>(DapperSql.GetUserProfile, new
                {
                    @id = UserId,
                })).SingleOrDefault();
                if (GetUserProfile != null)
                {
                    return GetUserProfile;
                }
                return null;
            }
        }
        #endregion

        #region Search Available buse
        public List<BusTravelScheduleModel> SearchAvailableBuses(FindBus SearchDetails)
        {
            using (var connections = Connection)
            {
                if (SearchDetails.DateTime >=DateTime.Now)
                {
                    var BusDetailList = (connections.Query<BusTravelScheduleModel>(DapperSql.FindBusesWithDate,
                    new
                    {
                        @source = SearchDetails.Source,
                        @destination = SearchDetails.Destination,
                        @date = SearchDetails.DateTime
                    })).ToList();
                    return BusDetailList;

                }
                else
                {
                    var BusDetailList = (connections.Query<BusTravelScheduleModel>(DapperSql.FindBusesWithoutDate,
                    new
                    {
                        @source = SearchDetails.Source,
                        @destination = SearchDetails.Destination,

                    })).ToList();
                    return BusDetailList;
                }
                return null;
            }
                
        }
        #endregion


        #region Get booked Seat number
        public List<BookTicket> GetBookedSeatno(int travelId)
        {
            using (var connections = Connection)
            {
                if (travelId>0)
                {
                    var SeatlList = (connections.Query<BookTicket>(DapperSql.GetSeatNo,
                    new
                    {
                        @travelid= travelId
                    })).ToList();
                    return SeatlList;

                }
                
                return null;
            }

        }
        #endregion

        #region Insert Passenger Details
        public bool InsertPassengerDeatil(PassengerDetails passengerDetails)
        {
            bool IsInserted=false;
            using (var connections = Connection)
            {
                if (passengerDetails.TravelId > 0)
                {
                    var SeatlList = (connections.Query<PassengerDetails>(DapperSql.InsertPasengerDeatils,
                    new
                    {
                        @userid=passengerDetails.UserId,
                        @travelid = passengerDetails.TravelId,
                        @passengername =passengerDetails.PassengerName,
                        @age=passengerDetails.Age,
                        @seatno=passengerDetails.Seatno,
                       
                    }));
                    IsInserted = true;

                }

                return IsInserted;
            }

        }
        #endregion

        #region View Passenger Details
        public List<PassengerDetails> ViewPassengerDetail(PassengerDetails passengerDetails)
        {
            
            using (var connections = Connection)
            {
                if (passengerDetails.ticketId > 0)
                {
                    var PassengerList = (connections.Query<PassengerDetails>(DapperSql.ViewPasengerDeatils,
                    new
                    {
                        @referenceid = passengerDetails.referenceId,
                        @ticketid=passengerDetails.ticketId,
                        @userid = passengerDetails.UserId,
                        
                    }).ToList());

                    return PassengerList;
                }

                return null;
            }

        }
        #endregion

        #region  Get ticket list
        public List<PassengerDetails> GetTicketList(int travelId)
        {

            using (var connections = Connection)
            {
                if (travelId > 0 )
                {
                    var ticketList = (connections.Query<PassengerDetails>(DapperSql.GetTicketlist,
                    new
                    {
                        @travelid = travelId,
                       
                        

                    }).ToList());

                    return ticketList;
                }

                return null;
            }

        }
        #endregion

        #region  Delete Bus Travel
        public bool DeleteBusTravel(int travelId)
        {

            using (var connections = Connection)
            {
                if (travelId > 0)
                {
                    var ticketList = (connections.Query<PassengerDetails>(DapperSql.DeleteBusTravel,
                    new
                    {
                        @travelid = travelId,
                       


                    }));

                    return true;
                }

                return false;
            }

        }
        #endregion

        #region Insert Payment Details
        public List<PaymentModel> InsertPaymentDetail(PaymentModel paymentDetail)
        {
            bool IsInserted = false;
            using (var connections = Connection)
            {
                if (paymentDetail.TravelId > 0)
                {
                    Random generator = new Random();
                    String reference = generator.Next(0, 1000000).ToString("D6");
                    var SeatlList = (connections.Query<PassengerDetails>(DapperSql.InsertPaymentDeatils,
                    new
                    {
                        @userid=paymentDetail.UserId,
                        @travelid=paymentDetail.TravelId,
                        @busid=paymentDetail.BusId,
                        @totalseatreserved=paymentDetail.TotalSeatsReserved,
                        @totalamount=paymentDetail.TotalAmount,
                        @holdername=paymentDetail.HolderName,
                        @cardnumber=paymentDetail.CardNumber,
                        @referenceid=reference,
                        @cardtype=paymentDetail.SelectedPaymentMethod,
                        
                    }));
                    var updaterefid = (connections.Query<TicketDetail>(DapperSql.UpdateRef,
                    new
                    {
                        @userid = paymentDetail.UserId,
                        @travelid = paymentDetail.TravelId,
                       
                        @referenceid = reference,
                    }));
                    var Ticketdetail = (connections.Query<PaymentModel>(DapperSql.Downloadticket,
                    new
                    {
                        @userid = paymentDetail.UserId,
                        @travelid = paymentDetail.TravelId,
                        @busid = paymentDetail.BusId,
                        @referenceid= reference,

                    }).ToList());
                    return Ticketdetail;
                }

                return null;
            }

        }
        #endregion

        #region Get Fare
        public PaymentModel GetFare(int travelId)
        {
            using(var connections=Connection)
            {
                if(travelId>0)
                {
                    var fare = connections.Query<PaymentModel>(DapperSql.GetFare,
                    new
                    {
                        @travelid = travelId,
                    }).SingleOrDefault();
                    return fare;
                }
                return null;
            }
        }

        #endregion

        #region Cancel Ticket
        public bool CancelTicket(int passengerId)
        {
            bool IsCanceled = false;
            using (var connections = Connection)
            {
                if (passengerId > 0)
                {
                    var cancelticket = (connections.Query<int>(DapperSql.CancelTicket,
                    new
                    {
                        @passengerid=passengerId,

                    }));
                    IsCanceled = true;

                }

                return IsCanceled;
            }

        }
        #endregion

        #region Get Refetence Id
        public FindBus GetReferenceId(int travelId, int busId, int userId)
        {

            using (var connections = Connection)
            {
                if (travelId > 0 && busId > 0)
                {
                    var ticket = (connections.Query<FindBus>(DapperSql.GetReferenceId, new
                    {
                        @userid = userId,
                        @travelid = travelId,
                        @busid = busId,
                    })).SingleOrDefault();
                    
                   

                    return ticket;
                }

                return null;
            }

        }
        #endregion
        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        
        
    }
}
