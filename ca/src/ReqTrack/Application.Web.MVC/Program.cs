using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ReqTrack.Application.Web.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var initializer = new Runtime.Core.Initialization.Initializer();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
