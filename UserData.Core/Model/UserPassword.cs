using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Core.Model
{
    public class UserPassword
    {
        public string HashPassword { get; set; }

        public byte[] SaltPassword { get; set; }
    }
}
