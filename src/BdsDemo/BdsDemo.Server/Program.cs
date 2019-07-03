using BlazorDesktopShell;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BdsDemo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            StartupExtensions.Run(BuildWebHost(args));
            //using IBdsWindow mainWindow = StartupExtensions.BdsMainWindow;
            //mainWindow.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>()
                .Build();
    }
}
