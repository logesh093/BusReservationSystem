using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class PassengerDetails
    {
        public int PassengerId { get; set; } 
        public bool IsCanceled {  get; set; }
        public int TravelId { get; set; }
        public int UserId { get; set; }
        public int BusId { get; set; }
        public string? PassengerName { get; set; }
        public int Age { get; set;}
        public int Seatno { get; set;}
        //public int TicketId { get; set;}
        public int Fare { get; set;}
        public List<BookTicket>? seatnoList { get; set; }
        public int Count { get; set; }
        public int passengercount {  get; set; }
        public int IsSeatAvailable { get; set; }
        public int referenceId { get; set; }
        public int ticketId { get; set;}
        
    }
}
