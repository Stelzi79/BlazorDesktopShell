using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorDesktopShell
{
    public static class BdsHostExtensions
    {
        public static IBdsHostBuilder UseConfiguration(this IBdsHostBuilder host, IConfiguration configuration)
        {
            foreach (KeyValuePair<string, string> setting in configuration.AsEnumerable(makePathsRelative: true))
            {
                host.UseSetting(setting.Key, setting.Value);
            }
            return host;


        }
        public static IBdsHostBuilder UseStartup<TStartup>(this IBdsHostBuilder hostBuilder) where TStartup : class => hostBuilder.UseStartup(typeof(TStartup));
        public static IBdsHostBuilder UseStartup(this IBdsHostBuilder hostBuilder, Type startupType)
        {
            string startupAssemblyName = startupType.GetTypeInfo().Assembly.GetName().Name;

            hostBuilder.UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyName);


            //Light up the GenericHostBuilder implementation
            if (hostBuilder is ISupportsStartup supportsStartup)
            {
                return (IBdsHostBuilder)supportsStartup.UseStartup(startupType);

            }
            else
                return hostBuilder;
        }
    }
}
