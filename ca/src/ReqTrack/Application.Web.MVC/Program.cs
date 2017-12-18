using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
