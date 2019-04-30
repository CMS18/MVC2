using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteDemo.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Show", "Course", new { id = 1234 });
        }
    }
}
