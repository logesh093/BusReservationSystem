using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class UserLogin
    {
        public string UserEmailid { get; set; }
        public string? UserPassword { get; set; }
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
    }
}
