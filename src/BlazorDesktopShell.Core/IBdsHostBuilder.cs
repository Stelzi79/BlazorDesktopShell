using Microsoft.Extensions.Hosting;

namespace BlazorDesktopShell
{
    public interface IBdsHostBuilder : IHostBuilder
    {
        new IBdsHost Build();
        void UseSetting(string key, object value);
    }
}