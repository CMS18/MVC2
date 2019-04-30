using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVC
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return Content("<h1>Hello world</h1>", "text/html");
        }

        public IActionResult UseView()
        {
            return View();
        }

    }
}
