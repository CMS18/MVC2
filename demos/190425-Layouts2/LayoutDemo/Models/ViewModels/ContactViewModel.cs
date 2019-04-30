using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayoutDemo.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(40)]
        [DataType(DataType.Password)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Tel { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
