using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class UserDetailList
    {
        public int BusId {  get; set; }
        public int TravelId { get; set; }
        public int ticketId { get; set; }
        public int UserId { get; set; }
        public string? BusName { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public DateTime DateTime { get; set; }
        public int numberOfSeats { get; set; }
        public int referenceNumber { get; set; }
    }
}
