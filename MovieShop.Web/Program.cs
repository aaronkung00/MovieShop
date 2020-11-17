using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

// Kestrel Server...
// Main method is the entry point which will create a hosting environment so that
// ASP.NET Core its gonna call a calss called Starup -->
// If something is wrong or not working first to look for is Startup Class

// Middleware in ASP.NET Core
// When you make a request in ASP.MVC/API... the request will go through some middleware
// eg.
// Request -> M1 -> some processing, --> M2 -- processing --> Destination 
// Response --> M3 --> M2 --> M1 
// ASP.NET Core has some built-in middlewares where every request will go through
// those middlewares. We can also create our own Middlewares and plugin to pipeline

// http://example.com/Students/Index ==> GET
// StudentContorller class and Index Action method

// Routing --> Pattern matching technique
// Traditional based routing / Conventional based routing
//  http://example.com/Students/Index ==> GET
//  http://example.com/ ==> GET
//  http://example.com/Movies ==> GET
//  http://example.com/Movies/Create ==> GET
//  http://example.com/Movies/Details/22 ==> GET
//  Attribute based routing --> Most used one