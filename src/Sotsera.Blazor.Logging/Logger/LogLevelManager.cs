using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Logging.Configuration;

namespace Sotsera.Blazor.Logging.Logger
{
    internal class LogLevelManager : ILogLevelManager
    {
        public const string Path = "Blazor:Logging";
        public const string LogLevelPath = "Blazor:Logging:LogLevel:Default";
        private LogLevel _currentLogLevel;

        internal IList<BlazorConfigurationProvider> Providers { get; }
        public string[] ValidLogLevels => Enum.GetNames(typeof(LogLevel));

        public LogLevelManager(LogLevel initialLevel)
        {
            _currentLogLevel = initialLevel;
            Providers = new List<BlazorConfigurationProvider>();
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

    }
}