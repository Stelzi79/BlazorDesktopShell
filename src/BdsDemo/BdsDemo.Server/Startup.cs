using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using BlazorDesktopShell;
using System.Linq;
using Chromely.Core;

namespace BdsDemo.Server
{
    public class Startup
    {
        private readonly IWebHostEnvironment _Env;
        private readonly IConfiguration _Config;
        //private readonly ILoggerFactory _loggerFactory;

        public Startup(IWebHostEnvironment env, IConfiguration config/*, ILoggerFactory loggerFactory*/)
        {
            _Env = env;
            _Config = config;
            //_loggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" }));
            services.AddBlazorDesktopShell(_Config);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBdsWindow window, ChromelyConfiguration chromelyConf)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
            app.UseBlazorDesktopShell(env, window, chromelyConf);
        }
    }
}
