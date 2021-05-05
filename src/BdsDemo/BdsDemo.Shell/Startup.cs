using BlazorDesktopShell;
using Chromely.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BdsDemo.Shell
{
    public class Startup
    {
        private readonly IHostEnvironment _Env;
        private readonly IConfiguration _Config;
        //private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostEnvironment env, IConfiguration config/*, ILoggerFactory loggerFactory*/)
        {
            _Env = env;
            _Config = config;
            //_loggerFactory = loggerFactory;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            System.Console.WriteLine("test");
        }
        public void Configure(IBdsHostBuilder hostBuilder, IHostEnvironment env, /*ChromelyConfiguration chromelyConf,*/ IHostApplicationLifetime lifetime)
        {
        }

    }
}