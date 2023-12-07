using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class DownloadTicket
    {
        public string Source {  get; set; } 
        public string BusName { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int ReferenceId { get; set; }
        public List<PassengerList> passengerLists { get; set; }
    }
}
