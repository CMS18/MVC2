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
using Microsoft.AspNetCore.DataProtection;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private IDataProtector protector;

        public HomeController(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("MyLogin");
        }

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
                var username = protector.Unprotect(loginCookie);
                var user = new GenericPrincipal(new GenericIdentity(username), null);
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

                Response.Cookies.Append("Login", protector.Protect(model.UserName),
                    new CookieOptions {
                        Expires = DateTimeOffset.Now.AddDays(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax
                    });

            }

            return View(model);
        }

        public IActionResult Protect(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["Result"] = protector.Protect(value);
            }

            return View();
        }

        public IActionResult Unprotect(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ViewData["Result"] = protector.Unprotect(value);
            }

            return View();
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
