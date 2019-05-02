using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityFromEmpty.Models
{
    public class LoginUserModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}
