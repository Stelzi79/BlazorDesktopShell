using Chromely.Core.Host;

namespace BlazorDesktopShell
{
    public interface IBdsWindow : IChromelyWindow
    {
        int Run();
    }
}