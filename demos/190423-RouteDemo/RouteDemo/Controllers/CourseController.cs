using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouteDemo.Models;
using RouteDemo.Services;

namespace RouteDemo.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult ShowAllCourses()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            // Hämta från databasen repo.Get(id)
            var course = CourseRepository.Get(id);

            var model = new CourseEditViewModel();
            model.Titel = course.Titel;
            model.Description = course.Description;


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, 
            [FromForm] CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                // Spara till databas

                return RedirectToAction("Edit");
            }

            return View(model);
        }

    }
}