using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailDemo.Web.Models
{
    public class AddUserModel
    {
        [Required]
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
