using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloHttp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // app.UseMyMiddleware();

            app.Run(async (context) =>
            {
                Console.WriteLine("Incoming Request Start");

                // HttpContext
                var path = context.Request.Path;
                context.Request.Query.TryGetValue("name", out var name);

                //context.Request

                // Använd HttpResponse för att sätta statuskod och skicka ett svar.
                context.Response.StatusCode = 200;
                // Skicka ett svar
                await context.Response.WriteAsync($"<h1>Hello {name}!</h1>");


                Console.WriteLine("Sedning Response");
            });
        }
    }
}
