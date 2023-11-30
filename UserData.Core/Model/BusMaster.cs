using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class BusMaster
    {
        public int BusId {  get; set; }
        public string? BusName { get; set; }
        public int? WindowSeats { get; set; }
        public int? NonWindowSeats {  get; set; }
        public bool IsDelated { get; set; }
        public DateTime CreateTimeStamp {get; set;}
        public DateTime UpdateTimeStamp {get; set;}
    }
}
