using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Logging.Configuration;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class LogManager : ILogManager
    {
        public const string Path = "Blazor:Logging";
        public const string LogLevelPath = "Blazor:Logging:LogLevel:Default";
        private LogLevel _currentLogLevel;

        internal IList<BlazorConfigurationProvider> Providers { get; }
        public event Action<string, string> OnLog;
        public string[] ValidLogLevels => Enum.GetNames(typeof(LogLevel));
        public string Version { get; }

        public GroupScope CurrentScope { get; set; }

        public LogManager(LogLevel initialLevel)
        {
            _currentLogLevel = initialLevel;
            Providers = new List<BlazorConfigurationProvider>();
            Version = InspectAssemblyVersion();
        }

        public LogLevel CurrentLevel
        {
            get => _currentLogLevel;
            set
            {
                if (_currentLogLevel == value) return;

                foreach (var provider in Providers)
                {
                    provider.SetLevel(LogLevelPath, value);
                }

                _currentLogLevel = value;
            }
        }

        public string CurrentLevelName
        {
            get => Enum.GetName(typeof(LogLevel), _currentLogLevel);
            set => CurrentLevel = (LogLevel)Enum.Parse(typeof(LogLevel), value);
        }

        internal IConfigurationSection ConfigurationSection()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> { { LogLevelPath, CurrentLevelName } })
                .Add(new BlazorConfigurationSource(this))
                .Build()
                .GetSection(Path);
        }

        public void RaiseLogEvent(string level, string message)
        {
            OnLog?.Invoke(level, message);
        }

        private string InspectAssemblyVersion()
        {
            return GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
        }
    }
}