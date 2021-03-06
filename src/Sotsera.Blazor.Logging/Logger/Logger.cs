﻿using System;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class Logger : ILogger
    {
        // Find out a better way for handling the scope context for parallel work
        //private readonly AsyncLocal<GroupScope> _currentScope = new AsyncLocal<GroupScope>();
        public string Name { get; }
        public IJSRuntime JsRuntime { get; }
        public LogManager LogManager { get; }

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public Logger(string name, IJSRuntime jsRuntime, LogManager logManager)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            JsRuntime = jsRuntime;
            LogManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            var currentScope = LogManager.CurrentScope;
            return LogManager.CurrentScope = new GroupScope(this, LogManager, state?.ToString(), currentScope);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null) return;

            var logMessage = $"{logLevel.ToString()} - {Name} - {message}";

            if (exception != null) logMessage += $" - {exception.Message}";

            LogManager.CurrentScope?.EnsureHasBeenShown();

            Log(Enum.GetName(typeof(LogLevel), logLevel), logMessage);
        }

        internal void Log(string logLevel, string logMessage)
        {
            LogManager.RaiseLogEvent(logLevel, logMessage);
            JsRuntime.InvokeAsync<object>("sotsera.blazor.logging.log", logLevel, logMessage);
        }

        internal void LogGroupEnd(string label)
        {
            LogManager.RaiseLogEvent("GroupEnd", label);
            LogManager.CurrentScope = LogManager.CurrentScope?.Parent;
            JsRuntime.InvokeAsync<object>("sotsera.blazor.logging.log", "GroupEnd");
        }
    }
}
