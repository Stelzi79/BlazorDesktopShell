using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace BlazorDesktopShell
{
    public interface IHostingStartup
    {
        void Configure(IBdsHostBuilder hostBuilder, IHostEnvironment env, /*ChromelyConfiguration chromelyConf,*/ IHostApplicationLifetime lifetime);

    }

}
