using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace BlazorDesktopShell
{
    public enum ApplicableSettings
    {
        applicationName,
        contentRoot,
        environment,
        HostingStartupType

    }
    public class BdsHostBuilder : HostBuilder, IBdsHostBuilder, ISupportsStartup
    {
        private readonly IDictionary<string, object> _InternalSettings = new Dictionary<string, object>();

        public BdsHostBuilder() : base()
        {

        }
        public new IBdsHost Build()
        {
            var hostingStartup = (IHostingStartup)Activator.CreateInstance((Type)_InternalSettings["HostingStartupType"]);
            //hostingStartup.Configure(this);


            return new BdsHost(base.Build(), _InternalSettings);
        }

        public IHostBuilder Configure(Action<HostBuilderContext, IApplicationBuilder> configure) => throw new NotImplementedException();

        public void UseSetting(string key, object value)
        {
            if (Enum.GetNames(typeof(ApplicableSettings)).Contains(key))
            {
                _InternalSettings.Add(key, value);
            }
        }

        public IHostBuilder UseStartup(Type startupType)
        {
            UseSetting(ApplicableSettings.HostingStartupType.ToString(), startupType);
            //var hostingStartup = (IHostingStartup)Activator.CreateInstance(startupType);


            //return ConfigureServices(hostingStartup)
            return this;
        }

        internal string? GetSetting(string key) => _InternalSettings.ContainsKey(key) ? _InternalSettings[key].ToString() : null;
    }
}