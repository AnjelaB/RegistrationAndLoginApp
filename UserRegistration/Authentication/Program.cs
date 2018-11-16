using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Authentication";

            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds wbe host
        /// </summary>
        /// <param name="args"> args </param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
