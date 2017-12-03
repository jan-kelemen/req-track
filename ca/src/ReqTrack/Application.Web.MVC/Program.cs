using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReqTrack.Runtime.Core.Configuration;

namespace ReqTrack.Application.Web.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationInitializer = new Initializer();
            configurationInitializer.Initialize();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
