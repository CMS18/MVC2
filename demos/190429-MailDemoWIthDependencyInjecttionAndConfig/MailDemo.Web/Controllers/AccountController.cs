using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailDemo.Application;
using MailDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailSender emailSender;

        public AccountController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Skapa konto - slumpa lösenord
                var password = "P@ssw0rd";

                // Skicka mejl till användare
                var htmlMessage = $@"<h1>Här är ditt lösen</h1>
                                <p>Ditt lösenord är: <b>{password}</b>
                                <p>Med vänlig hälsning, My Cool Site</p>";

                emailSender.SendEmail(model.Email, "noreply@coolsystem.com", "Här är ditt lösenord", htmlMessage);


                // Bekräfta att kontot skapats
                TempData["success"] = $"Ett konto för <b>{model.Email}</b> har skapats.";
                return RedirectToAction("Index");
            }

            // Fel på valideringen - Visa formuläret igen!
            return View("Index", model);
        }

    }
}