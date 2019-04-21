using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Sotsera.Blazor.Logging.Logger
{
    [ProviderAlias("Blazor")]
    internal class LoggerProvider : ILoggerProvider
    {
        public IJSRuntime JsRuntime { get; }
        public LogManager LogManager { get; }
        private ConcurrentDictionary<string, Logger> Loggers { get; }

        public LoggerProvider(IJSRuntime jsRuntime, LogManager logManager)
        {
            JsRuntime = jsRuntime;
            LogManager = logManager;
            Loggers = new ConcurrentDictionary<string, Logger>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return Loggers.GetOrAdd(categoryName, name => new Logger(name, JsRuntime, LogManager));
        }

        public void Dispose()
        {
            Loggers.Clear();
        }
    }
}