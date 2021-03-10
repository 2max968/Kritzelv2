using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kritzel.WebCast.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kritzel.WebCast
{
    public class Program
    {
        static bool running = true;

        public static void Main(string[] args)
        {
            new Thread(TimerLoop).Start();
            CreateHostBuilder(args).Build().Run();
            running = false;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void TimerLoop()
        {
            while(running)
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    if (!running) break;
                }
                HomeController.UpdateCasts();
            }
            Console.WriteLine("Timer stopped");
        }
    }
}
