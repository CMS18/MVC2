using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayoutDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LayoutDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            var model = new ContactViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Model Binding Without Errors

                // Skicka mejl
                Console.WriteLine($"Skickar mejl: {model.Name} <{model.Email}> {model.Tel} {model.Message}");




                return View(model);
            }
            else
            {
                // Errors in binding
                var errors = new StringBuilder();
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        errors.Append($"<li>{error.ErrorMessage}</li>");
                    }
                }
                ViewData["Errors"] = errors.ToString();

                return View(model);
            }



        }
    }
}