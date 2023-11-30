using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class FindBus
    {
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public DateTime? DateTime { get; set; }
        public int? ReferenceId { get; set; }
    }
}
