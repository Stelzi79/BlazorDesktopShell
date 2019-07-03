using System;
using System.Collections.Generic;
using System.Text;
using Chromely.Core;
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
            string startUrl = "https://google.com";
            string[] args = new string[0];



            ChromelyConfiguration config = ChromelyConfiguration
                            .Create()
                            .WithHostMode(WindowState.Normal, true)
                            .WithHostTitle("chromely")
                            .WithHostIconFile("chromely.ico")
                            .WithAppArgs(args)
                            .WithHostSize(1000, 600)
                            .WithStartUrl(startUrl);


            services.AddSingleton<ChromelyConfiguration>(config);
            services.AddSingleton<IBdsWindow>(BdsWindow.Create(config));
        }


        public static void UseBlazorDesktopShell(this IApplicationBuilder app, IWebHostEnvironment env, IBdsWindow window, ChromelyConfiguration chromelyConf) => window.Run(chromelyConf.AppArgs);

    }
}
