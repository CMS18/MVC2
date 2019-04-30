using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FirstMVC
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public MyLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Request Pipeline Start");

            await _next(httpContext);

            Console.WriteLine("Response Pipeline Done");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyLoggingMiddleware>();
        }
    }
}
