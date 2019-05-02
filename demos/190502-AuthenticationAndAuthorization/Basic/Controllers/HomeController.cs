using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basic.Models;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var text = ""; //System.IO.File.ReadAllText(@"C:\Users\FredrikHaglund\Desktop\fredriksfile.txt");

            ViewData["FredriksFil"] = text;

            var user = new GenericPrincipal(
                new GenericIdentity("Kalle Blomkvist"),
                new[]{ "Admin", "Expert" }
                );

            HttpContext.User = user;

            return View();
        }

        public IActionResult Basic()
        {
            var authHeader = Request.Headers["authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                // WWW-Authenticate: Basic realm="User Visible Realm"
                Response.Headers.Add("WWW-Authenticate", "Basic realm=\"login\"");
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            var parts = authHeader.ToString().Split(" ");
            var decodedtextparts = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1])).Split(":");
            var username = decodedtextparts[0];
            var password = decodedtextparts[1];

            // TODO: Kolla att lösenordet stämmer!!!

            var user = new GenericPrincipal(new GenericIdentity(username), null);
            HttpContext.User = user;

            return View();
        }

        public IActionResult CookieLogin()
        {
            var model = new LoginModel();

            var loginCookie = Request.Cookies["Login"];
            if (loginCookie != null)
            {
                var user = new GenericPrincipal(new GenericIdentity(loginCookie), null);
                HttpContext.User = user;
            }


            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CookieLogin(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                //TODO: Kolla att lösenordet stämmer

                Response.Cookies.Append("Login", model.UserName,
                    new CookieOptions {
                        Expires = DateTimeOffset.Now.AddDays(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax
                    });

            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
