using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class TicketDetail
    {
        public List<PaymentModel> ticketdetails { get; set; }
        public int? ReferenceId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime? Date { get; set; }
        public int busId { get; set; }
        public int travelId { get; set; }
        public int userId { get; set; }

    }
}
