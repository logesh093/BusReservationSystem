using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class BusTravelScheduleModel
    {
        public int TravelId { get; set; }

        public int BusId { get; set; }
        
        public string? Source { get; set; }

        public string? Destination { get; set; }
        public string? BName { get; set; }

        public int Fare { get; set; }

        public DateTime DepatureDateTime { get; set; }
        public int SeatsAvailable {  get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime Update_Time_Stamp { get; set; }
        public DateTime Create_Time_Stamp { get; set; }
    }
}
