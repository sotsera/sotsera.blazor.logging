using System;
using Microsoft.Extensions.Logging;

namespace Sotsera.Blazor.Logging
{
    public interface ILogManager
    {
        event Action<string, string> OnLog;
        string Version { get; }
        LogLevel CurrentLevel { get; set; }
        string CurrentLevelName { get; set; }
        string[] ValidLogLevels { get; }
    }
}