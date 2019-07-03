using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Chromely.CefGlue;
using Chromely.Core;
using Chromely.Core.Helpers;
using Chromely.Core.Host;
using Chromely.Core.Infrastructure;

namespace BlazorDesktopShell
{
    public class BdsWindow : IBdsWindow
    {
        private readonly IChromelyWindow _BaseWindow;

        private static IBdsWindow _Instance;
        private BdsWindow(IChromelyWindow baseWindow) => _BaseWindow = baseWindow;

        public static IBdsWindow Create(ChromelyConfiguration conf)
        {
            if (_Instance == null)
            {
                _Instance = new BdsWindow(ChromelyWindow.Create(conf));
            }
            return _Instance;
        }


        #region IChromelyWindow


        public object Browser => _BaseWindow.Browser;

        public ChromelyConfiguration HostConfig => _BaseWindow.HostConfig;
        public void Close() => _BaseWindow.Close();
        public void Dispose() => _BaseWindow.Dispose();
        public void Exit() => _BaseWindow.Exit();
        public void RegisterCustomHandler(CefHandlerKey key, Type implementation) =>
            _BaseWindow.RegisterCustomHandler(key, implementation);
        public void RegisterEventHandler<T>(CefEventKey key, EventHandler<T> handler) =>
            _BaseWindow.RegisterEventHandler<T>(key, handler);
        public void RegisterEventHandler<T>(CefEventKey key, ChromelyEventHandler<T> handler) =>
            _BaseWindow.RegisterEventHandler<T>(key, handler);
        public void RegisterServiceAssemblies(string folder) =>
            _BaseWindow.RegisterServiceAssemblies(folder);
        public void RegisterServiceAssemblies(List<string> filenames) =>
            _BaseWindow.RegisterServiceAssemblies(filenames);
        public void RegisterServiceAssembly(string filename) =>
            _BaseWindow.RegisterServiceAssembly(filename);
        public void RegisterServiceAssembly(Assembly assembly) =>
            _BaseWindow.RegisterServiceAssembly(assembly);
        public void RegisterUrlScheme(UrlScheme scheme) =>
            _BaseWindow.RegisterUrlScheme(scheme);
        public int Run(string[] args) => _BaseWindow.Run(args);
        public void ScanAssemblies() => _BaseWindow.ScanAssemblies();
        #endregion

        public int Run() => _BaseWindow.Run(new string[0]);
    }
}
