using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class UserPage
    {
        public string? Name { get; set; }
        public string? EmailId { get; set; }

        public string? Address { get; set; }

        public string? Phonenumber { get; set; }
        
        public FindBus? findBus { get; set; }
        public List<UserDetailList>? userdetailList{ get; set; }

    }
}
