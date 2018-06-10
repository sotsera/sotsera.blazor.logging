using Microsoft.Extensions.Logging;

namespace Sotsera.Blazor.Logging
{
    public interface ILogLevelManager
    {
        LogLevel CurrentLevel { get; set; }
        string CurrentLevelName { get; set; }
        string[] ValidLogLevels { get; }
    }
}