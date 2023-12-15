using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Xml.Linq;
using UserData.Core.Model;

namespace BusReservationSystem.webSolution.Controllers
{
    public class BusSeatPrebookController : Controller
    {
        #region Login page
        public IActionResult UserOrAdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserOrAdminLoginVerification(UserLogin logindetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/UserOrAdminLoginVerification");
                var checkresult = client.PostAsJsonAsync<UserLogin>(client.BaseAddress, logindetail).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var loginStatus = checkresult.Content.ReadFromJsonAsync<UserLogin>().Result;
                    if (loginStatus.UserEmailid != null && loginStatus.IsAdmin == true)
                    {
                        TempData["Message"] = "Login Successfully..";
                        HttpContext.Session.SetString("IsAdmin", (loginStatus.IsAdmin).ToString());
                        return RedirectToAction("BusDashboard");
                    }
                    else if (loginStatus.UserEmailid != null && loginStatus.IsAdmin == false)
                    {
                        TempData["Message"] = "Login Successfully..";
                        HttpContext.Session.SetString("IsAdmin", (loginStatus.IsAdmin).ToString());
                        HttpContext.Session.SetString("userId", (loginStatus.UserId).ToString());
                        return RedirectToAction("UserDetailPage");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Email or Password!!!";

                        return View("UserOrAdminLogin");
                    }
                }
                return View("UserOrAdminLogin");
            }
        }
        #endregion


        #region Signup Page
        [HttpGet]
        public IActionResult UserOrAdminSignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertUserOrAdminDetails(UserOrAdminSignUp signupDetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/InsertUserOrAdminDetails");
                var checkresult = client.PostAsJsonAsync<UserOrAdminSignUp>(client.BaseAddress, signupDetail).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var signupStatus = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                    if (signupStatus == true)
                    {
                        TempData["SuccessMessage"] = "SignUp Successfully..";
                        return RedirectToAction("UserOrAdminLogin");
                    }
                    else
                    {
                        ViewBag.Message = "Email Already Exist!!!";

                        return View("UserOrAdminSignUp");
                    }
                }
                return View("UserOrAdminSignUp");
            }

        }
        #endregion

        #region User Page
        [HttpGet]
        public IActionResult UserDetailPage()
        {
            int userid = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            UserPage userProfile = new UserPage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/UserDetailPage");
                var Posttask = client.GetAsync("UserDetailPage?userId=" + userid);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    userProfile.userdetailList = checkresult.Content.ReadFromJsonAsync<List<UserDetailList>>().Result;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/UserProfile");
                var Posttask = client.GetAsync("UserProfile?userId=" + userid);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    UserPage GetUserProfile = checkresult.Content.ReadFromJsonAsync<UserPage>().Result;
                    userProfile.Name = GetUserProfile.Name;
                    userProfile.EmailId = GetUserProfile.EmailId;
                    userProfile.Address = GetUserProfile.Address;
                    userProfile.Phonenumber = GetUserProfile.Phonenumber;
                    
                    return View(userProfile);
                }
                return View();
            }
        }
        #endregion

        [HttpGet]
        public IActionResult _PartialViewProfile()
        {
            using (var client = new HttpClient())
            {
                int userid = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/UserProfile");
                var Posttask = client.GetAsync("UserProfile?userId=" + userid);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    UserPage GetUserProfile = checkresult.Content.ReadFromJsonAsync<UserPage>().Result;


                    return PartialView("_PartialViewProfile",GetUserProfile);
                }
                return View();
            }
        }

        #region View Passenger Details
        [HttpGet]
        public IActionResult ViewPassengerDetail(int referenceId, int ticketId)
        {
            PassengerDetails passengerDetails = new PassengerDetails();

            passengerDetails.referenceId = referenceId;
            passengerDetails.ticketId = ticketId;
            passengerDetails.UserId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/ViewPassengerDetail");
                var checkresult = client.PostAsJsonAsync<PassengerDetails>(client.BaseAddress, passengerDetails).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var passengerList = checkresult.Content.ReadFromJsonAsync<List<PassengerDetails>>().Result;
                    if (passengerList != null)
                    {
                       
                        View(passengerList);
                    }

                }

                return View();
            }
        }
        [HttpGet]
        public IActionResult CancelTicket(int passengerId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/CancelTicket");
                var Posttask = client.GetAsync("CancelTicket?passengerId=" + passengerId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    bool IsCanceled = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                    if (IsCanceled)
                    {
                        return RedirectToAction("UserDetailPage");
                    }
                }
                return View(false);
            }
        }


        #endregion

        #region Get Bus DashBoard
        [HttpGet]
        public IActionResult BusDashboard()
        {
            TempData["IsAdmin"] = HttpContext.Session.GetString("IsAdmin");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusDashboard");

                var Posttask = client.GetAsync(client.BaseAddress);

                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var getBusList = checkresult.Content.ReadFromJsonAsync<List<BusMaster>>().Result;

                    return View(getBusList);
                }
                return View();

            }
        }
        #endregion

        #region Add or Update Bus
        [HttpGet]
        public IActionResult AddOrUpdateBus(int busId)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusDetailById");
                var Posttask = client.GetAsync("GetBusDetailById?busId=" + busId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    BusMaster BusDetails = checkresult.Content.ReadFromJsonAsync<BusMaster>().Result;
                    return View(BusDetails);

                }
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddOrUpdateBus(BusMaster busDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/AddOrUpdateBus");
                var Posttask = client.PostAsJsonAsync<BusMaster>(client.BaseAddress, busDetails);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var result = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                    if (result == true)
                    {
                        TempData["Message"] = "Bus Added Successfully...";
                    }
                    else
                    {
                        TempData["Message"] = "Detail Updated Successfully...";
                    }
                }
                return RedirectToAction("BusDashboard");
            }

        }
        #endregion

        #region Get Bus Travel Schedule by busid
        [HttpGet]
        public IActionResult GetBusTravelSchedule(int busId)
        {
            if (HttpContext.Session.GetString("busId")!=null && busId==0)
            {
                busId = Convert.ToInt32(HttpContext.Session.GetString("busId"));
            }
            if (busId > 0)
            {
                HttpContext.Session.SetString("busId", busId.ToString());
                //TempData["busId"] = busId;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusDetailById");
                    var Posttask = client.GetAsync("GetBusDetailById?busId=" + busId);
                    Posttask.Wait();
                    var checkresult = Posttask.Result;
                    if (checkresult.IsSuccessStatusCode)
                    {
                        BusMaster BusDetails = checkresult.Content.ReadFromJsonAsync<BusMaster>().Result;
                        HttpContext.Session.SetString("busname", BusDetails.BusName.ToString());

                    }
                }
            }
            ViewBag.BusName = HttpContext.Session.GetString("busname"); 
            TempData["IsAdmin"] = HttpContext.Session.GetString("IsAdmin");
            busId = Convert.ToInt32(HttpContext.Session.GetString("busId"));
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusTravelSchedule");
                var Posttask = client.GetAsync("GetBusTravelSchedule?busId=" + busId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var BusSchedule = checkresult.Content.ReadFromJsonAsync<List<BusTravelScheduleModel>>().Result;

                    if (BusSchedule != null)
                    {
                        return View(BusSchedule);
                    }

                }
                return View();
            }
        }
        #endregion

        #region Get Bus Travel Schedule by travel id
        [HttpGet]
        public IActionResult GetBusScheduleByTravelId(int travelId)
        {

            using (var client = new HttpClient())
            {
                if (travelId > 0)
                {
                    client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusScheduleByTravelId");
                    var Posttask = client.GetAsync("GetBusScheduleByTravelId?travelId=" + travelId);
                    Posttask.Wait();
                    var checkresult = Posttask.Result;
                    if (checkresult.IsSuccessStatusCode)
                    {
                        BusTravelScheduleModel BusDetails = checkresult.Content.ReadFromJsonAsync<BusTravelScheduleModel>().Result;
                        if (BusDetails != null)
                        {
                            return View(BusDetails);
                        }

                    }
                }

                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateOrUpdateTravelId(BusTravelScheduleModel busDetails)//continue with
        {
            
            busDetails.BusId = Convert.ToInt32(HttpContext.Session.GetString("busId"));
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/CreateOrUpdateTravelId");
                var checkresult = client.PostAsJsonAsync<BusTravelScheduleModel>(client.BaseAddress, busDetails).Result;
                
                
                if (checkresult.IsSuccessStatusCode)
                {
                    bool result = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                    if (result == true && busDetails.BusId>0)
                    {
                        TempData["Message"] = "Detail Updated Successfully...";
                    }
                    else
                    {
                        TempData["Message"] = "Travel Id Created Successfully...";
                    }
                }
                return RedirectToAction("GetBusTravelSchedule", new { busId = busDetails.BusId });
            } 

        }
        #endregion

        #region Book Ticket
        [HttpGet]
        public IActionResult GetTicketCount(int ticketCount, int travelId)//restart
        {
            HttpContext.Session.SetString("TravelId", (travelId).ToString());

            PassengerDetails passengerDetails = new PassengerDetails();
            passengerDetails.Count = ticketCount;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBookedSeatno");
                var Posttask = client.GetAsync("GetBookedSeatno?travelId=" + travelId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    passengerDetails.seatnoList = checkresult.Content.ReadFromJsonAsync<List<BookTicket>>().Result;
                    //if (TempData["SelectedSeatno"] != null)
                    //{

                    //    BookTicket no = new BookTicket();
                    //    no.Seatno = Convert.ToInt32(TempData["SelectedSeatno"]);
                    //    passengerDetails.seatnoList.Add(no);
                    //}
                    return View(passengerDetails);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult GetTicketCount(PassengerDetails pasengerDetail)
        {
            if (pasengerDetail != null)
            {

                if (pasengerDetail.Fare == 0)
                {
                    TempData["ticketcount"] = pasengerDetail.Count + 1;
                }

                PassengerDetails PassengerDetails = new PassengerDetails();
                using (var client = new HttpClient())
                {
                    pasengerDetail.referenceId=Convert.ToInt32(HttpContext.Session.GetString("ReferenceId"));
                    pasengerDetail.UserId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                    client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/InsertPassengerDeatil");
                    var Posttask = client.PostAsJsonAsync<PassengerDetails>(client.BaseAddress, pasengerDetail);
                    Posttask.Wait();
                    var checkresult = Posttask.Result;
                    if (checkresult.IsSuccessStatusCode)
                    {
                        bool Isinserted = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                        if (pasengerDetail.Count > 0)
                        {
                            //TempData["SelectedSeatno"] = pasengerDetail.Seatno;
                            //using (var client1 = new HttpClient())
                            //{
                            //    pasengerDetail.TravelId = Convert.ToInt32(HttpContext.Session.GetString("TravelId"));
                            //    client1.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBookedSeatno");
                            //    var Posttask1 = client1.GetAsync("GetBookedSeatno?travelId=" + pasengerDetail.TravelId);
                            //    Posttask1.Wait();
                            //    var checkresult1 = Posttask1.Result;
                            //    if (checkresult1.IsSuccessStatusCode)
                            //    {
                            //        PassengerDetails.seatnoList = checkresult1.Content.ReadFromJsonAsync<List<BookTicket>>().Result;

                            //    }
                            //}
                            TempData["passengerCount"] = pasengerDetail.passengercount;
                            return RedirectToAction("GetTicketCount", new { ticketCount = pasengerDetail.Count, travelId = pasengerDetail.TravelId });
                        }

                        return RedirectToAction("PaymentMethod");
                    }
                }


            }

            return View();
        }


        #endregion

        #region Get ticket list
        [HttpGet]
        public IActionResult GetTicketList(int travelId)
        {
            using (var client = new HttpClient())
            {
                if (travelId > 0)
                {
                    client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetTicketList");
                    var Posttask = client.GetAsync("GetTicketList?travelId=" + travelId);
                    Posttask.Wait();
                    var checkresult = Posttask.Result;
                    if (checkresult.IsSuccessStatusCode)
                    {
                        List<PassengerDetails> TicketList = checkresult.Content.ReadFromJsonAsync<List<PassengerDetails>>().Result;

                        return View(TicketList);

                    }

                }
                return View();
            }

        }
        #endregion

        #region Delete bus travel
        [HttpGet]
        public IActionResult DeleteBusTravel(int travelId)
        {
            using (var client = new HttpClient())
            {
                if (travelId > 0)
                {
                    client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/DeleteBusTravel");
                    var Posttask = client.GetAsync("DeleteBusTravel?travelId=" + travelId);
                    Posttask.Wait();
                    var checkresult = Posttask.Result;
                    if (checkresult.IsSuccessStatusCode)
                    {
                        bool IsDeleted = checkresult.Content.ReadFromJsonAsync<bool>().Result;
                        if (IsDeleted)
                        {
                            TempData["Message"] = "Travel Deleted successfully!!!";
                            int busId = Convert.ToInt32(HttpContext.Session.GetString("busId"));
                            return RedirectToAction("GetBusTravelSchedule", new { busId });
                        }

                    }

                }
                return View();
            }

        }
        #endregion

        #region Payment page
        [HttpGet]
        public IActionResult PaymentMethod()
        {
            using (var client = new HttpClient())
            {
                int travelId = Convert.ToInt32(HttpContext.Session.GetString("TravelId"));
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetFare");
                var Posttask = client.GetAsync("GetFare?travelId=" + travelId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {

                    PaymentModel paymentModel = checkresult.Content.ReadFromJsonAsync<PaymentModel>().Result;
                    paymentModel.TotalSeatsReserved = Convert.ToInt32(TempData["ticketcount"]);
                    paymentModel.TotalAmount = paymentModel.TotalSeatsReserved * paymentModel.Fare;
                    return View(paymentModel);
                }
                return View(null);
            }


        }
        [HttpPost]
        public IActionResult PaymentSuccess(PaymentModel paymentDetail)
        {

            paymentDetail.TravelId = Convert.ToInt32(HttpContext.Session.GetString("TravelId"));
            paymentDetail.UserId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            paymentDetail.ReferenceId = Convert.ToInt32(HttpContext.Session.GetString("ReferenceId"));
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/InsertPaymentDetail");
                var checkresult = client.PostAsJsonAsync<PaymentModel>(client.BaseAddress, paymentDetail).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    bool IsInserted = checkresult.Content.ReadFromJsonAsync<bool>().Result;

                }
            }
            
            DownloadTicket ticketdetail = new DownloadTicket();
            int travelId = Convert.ToInt32(HttpContext.Session.GetString("TravelId"));
            using (var client = new HttpClient())
            {
                
                int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/DownloadTicket");
                var checkresult = client.GetAsync("DownloadTicket?travelId=" + travelId + "&userId=" + userId).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var ticket = checkresult.Content.ReadFromJsonAsync<DownloadTicket>().Result;
                    ticketdetail.Source = ticket.Source;
                    ticketdetail.Destination = ticket.Destination;
                    ticketdetail.Date = ticket.Date;
                    ticketdetail.ReferenceId = ticket.ReferenceId;
                    
                }
            }
            
            //TempData["busId"] = busId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBusName");
                var Posttask = client.GetAsync("GetBusName?travelId=" + travelId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    
                    var busDetails = checkresult.Content.ReadFromJsonAsync<BusMaster>().Result;
                    ticketdetail.BusName = busDetails.BusName;

                }
            }

            using (var client = new HttpClient())
            {
                int referenceId = Convert.ToInt32(HttpContext.Session.GetString("ReferenceId"));
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetPassenger");
                var checkresult = client.GetAsync("GetPassenger?referenceId=" + referenceId).Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    List<PassengerList> passengerlist = checkresult.Content.ReadFromJsonAsync<List<PassengerList>>().Result;
                    ticketdetail.passengerLists = passengerlist;
                    TempData["Message"] = "Ticket Booked Successfully..";
                    return View(ticketdetail);
                }
                return View();
            }
        }
        #endregion

        #region Search Available Bus


        [HttpPost]
        public IActionResult SearchAvailableBuses(FindBus SearchDetails)
        {
            Random generator = new Random();
            String reference = generator.Next(0, 1000000).ToString("D6");
            HttpContext.Session.SetString("ReferenceId", reference);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/SearchAvailableBuses");
                var Posttask = client.PostAsJsonAsync<FindBus>(client.BaseAddress, SearchDetails);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var BusList = checkresult.Content.ReadFromJsonAsync<List<BusTravelScheduleModel>>().Result;
                    return View(BusList);
                }
                return View();
            }

        }
        #endregion

        [HttpGet]
        public IActionResult _Partialview()
        {
            return PartialView("_Partialview");
        }


        [HttpPost]
        public JsonResult CheckSeatAvailability(int bookedSeats)
        {
            bool IscontainsItem = false;
            using (var client = new HttpClient())
            {
                int travelId = Convert.ToInt32(HttpContext.Session.GetString("TravelId"));
                client.BaseAddress = new Uri("http://localhost:5237/api/BusSeatPrebookAPI/GetBookedSeatno");
                var Posttask = client.GetAsync("GetBookedSeatno?travelId=" + travelId);
                Posttask.Wait();
                var checkresult = Posttask.Result;
                if (checkresult.IsSuccessStatusCode)
                {
                    var seatnoList = checkresult.Content.ReadFromJsonAsync<List<BookTicket>>().Result;
                    IscontainsItem = seatnoList.Any(item => item.Seatno == bookedSeats);

                    return Json(!IscontainsItem);
                }
            }

            return Json(IscontainsItem);
        }
    }
}