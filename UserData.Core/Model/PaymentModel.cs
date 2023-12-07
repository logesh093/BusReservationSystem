using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class PaymentModel
    {
        public string? SelectedPaymentMethod {  get; set; }
        public string? HolderName { get; set; }
        public string? CardNumber { get; set;}
        public string? BusName { get; set; }
        public int UserId { get; set; }
        public int TravelId { get; set;}
        public int BusId { get; set; }
        public int Fare { get; set; }
        public int TotalSeatsReserved { get; set; }
        public int TotalAmount {  get; set; }
        public int? CVV { get; set; } 
        public string? PassengerName { get; set; }
        public int Age { get; set; }
        public int Seatno { get; set; }
        public int ReferenceId {  get; set; }
        
    }
}
