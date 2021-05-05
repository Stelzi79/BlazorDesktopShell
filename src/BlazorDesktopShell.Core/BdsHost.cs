using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorDesktopShell
{
    public class BdsHost : IBdsHost
    {

        private readonly IHost _BaseHost;
        private readonly IDictionary<string, object> _InternalSettings;

        internal BdsHost(IHost baseHost, IDictionary<string, object> internalSettings)
        {
            _BaseHost = baseHost;
            _InternalSettings = internalSettings;
        }

        public static IBdsHostBuilder CreateDefaultBuilder(string[] args)
        {
            BdsHostBuilder builder = new BdsHostBuilder();
            if (string.IsNullOrEmpty(builder.GetSetting(HostDefaults.ContentRootKey)))
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            }
            if (args != null)
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            }

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                IHostEnvironment env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                if (env.IsDevelopment())
                {
                    Assembly appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            //.ConfigureLogging((hostingContext, logging) =>
            //{
            //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.AddEventSourceLogger();
            //})
            .UseDefaultServiceProvider((context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment());

            return builder;
        }

        #region IHost implementations
        public IServiceProvider Services => _BaseHost.Services;
        public void Dispose() => _BaseHost.Dispose();
        public Task StartAsync(CancellationToken cancellationToken = default) => _BaseHost.StartAsync(cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken = default) => _BaseHost.StopAsync(cancellationToken);
        #endregion
    }
}
