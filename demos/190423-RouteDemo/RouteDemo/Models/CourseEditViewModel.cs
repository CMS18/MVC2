using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouteDemo.Models
{
    public class CourseEditViewModel
    {
        [Required]
        public string Titel { get; set; }

        public string Description { get; set; }
    }
}
