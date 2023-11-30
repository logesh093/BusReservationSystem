using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
     public class UserOrAdminSignUp
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string DOB { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public string Phonenumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool Is_Admin { get; set; }
    }
}
