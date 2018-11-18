using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Sotsera.Blazor.Logging.Logger
{
    [ProviderAlias("Blazor")]
    internal class LoggerProvider : ILoggerProvider
    {
        public LogManager LogManager { get; }
        private ConcurrentDictionary<string, Logger> Loggers { get; }

        public LoggerProvider(LogManager logManager)
        {
            LogManager = logManager;
            Loggers = new ConcurrentDictionary<string, Logger>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return Loggers.GetOrAdd(categoryName, name => new Logger(name, LogManager));
        }

        public void Dispose()
        {
            Loggers.Clear();
        }
    }
}