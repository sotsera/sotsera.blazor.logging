using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using Microsoft.JSInterop;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class Logger : ILogger
    {
        public string Name { get; }

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;
        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public Logger(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null) return;

            var logMessage = $"{logLevel.ToString()} - {Name} - {message}";

            if (exception != null) logMessage += $" - {exception.Message}";

            JSRuntime.Current.InvokeAsync<object>("Sotsera.Blazor.Logging.log", Enum.GetName(typeof(LogLevel), logLevel), logMessage);
        }
    }
}
