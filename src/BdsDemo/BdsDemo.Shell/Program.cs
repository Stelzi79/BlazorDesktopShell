using System.Threading.Tasks;
using BlazorDesktopShell;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BdsDemo.Shell
{
    internal class Program
    {
        private static void Main(string[] args) => BuildBdsHost(args).Run();
        private static IBdsHost BuildBdsHost(string[] args) => BdsHost.CreateDefaultBuilder(args)
            .UseConfiguration(new ConfigurationBuilder()
                .AddCommandLine(args)
                //.AddEnvironmentVariables()
                .Build())
             .UseStartup<Startup>()
             .Build();

    }
}
