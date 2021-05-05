using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace BlazorDesktopShell
{

    internal interface ISupportsStartup
    {
        IHostBuilder Configure(Action<HostBuilderContext, IApplicationBuilder> configure);
        IHostBuilder UseStartup(Type startupType);
    }

}