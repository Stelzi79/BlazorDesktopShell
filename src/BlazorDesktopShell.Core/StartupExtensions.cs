using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chromely.Core;
using Chromely.Core.Helpers;
using Chromely.Core.Host;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BlazorDesktopShell
{
    public static class StartupExtensions
    {
        public static void AddBlazorDesktopShell(this IServiceCollection services, IConfiguration conf)
        {

            string startUrl = conf.GetSection("urls").Value;
            string[] args = new string[]{
                "--server.urls \"http://localhost:9901\""
            };
            ChromelyConfiguration config = ChromelyConfiguration
                            .Create()
                            .WithHostMode(WindowState.Normal, true)
                            .WithHostTitle("chromely")
                            .WithHostIconFile("chromely.ico")
                            .WithAppArgs(args)
                            .WithHostSize(1000, 600)
                            .WithSilentCefBinariesLoading(true)
                            .WithStartUrl(startUrl)
                            .UseDefaultWebsocketHandler(string.Empty, 9091, false)


                            //.WithLogSeverity(Chromely.Core.Infrastructure.LogSeverity.Verbose)
                            //.WithCustomSetting("StartWebSocket", false)
                            .WithLoadingCefBinariesIfNotFound(false);




            services.AddSingleton<ChromelyConfiguration>(config);
            //services.AddSingleton<IBdsWindow>(BdsWindow.Create(config));
        }

        public static void Run(IWebHost webHost)
        {
            //webHost.Run();
            webHost.RunAsync();
            while (_BdsMainWindow == null)
            {
                Task.Delay(new TimeSpan(0, 0, 1));
            }
            using IBdsWindow main = _BdsMainWindow;
            main.Run();


        }

        private static IBdsWindow _BdsMainWindow;
        public static void UseBlazorDesktopShell(this IApplicationBuilder app, IWebHostEnvironment env, ChromelyConfiguration chromelyConf)
        {

            _BdsMainWindow = BdsWindow.Create(chromelyConf);


            //_ = lifetime.ApplicationStarted.Register(() => OnAppStarted(app, env, chromelyConf));


            //TODO: Register other Lifetime events

        }
    }
}
