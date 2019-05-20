using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NextTrip.Models;

namespace NextTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            this.configuration = configuration;
            this.clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchSite(string q)
        {
            var model = new TypeAheadRoot();

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            // Bygg Url för anrop
            var urlBuilder = new UriBuilder("https://api.sl.se/api2/typeahead.json");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["key"] = configuration["api-key-typeahead"];
            query["searchstring"] = q;
            urlBuilder.Query = query.ToString();

            // Konstruera Request utifrån Url och Http Headers.
            var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClient");

            // Skapa en HttpClient (som en webbläsare fast utan gränssnitt)
            var client = clientFactory.CreateClient();

            // Detta gör det externa anropet
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<TypeAheadRoot>();
                if (isAjax)
                {
                    return PartialView(model);
                }
                else
                {
                    return View(model);
                };
            }

            return View("Index");
        }

        public IActionResult NextDeparture(int siteId)
        {
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
