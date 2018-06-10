using System;
using Microsoft.Extensions.Logging;

namespace Sotsera.Blazor.Logging.Configuration
{
    internal class BlazorConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
    {
        public void SetLevel(string path, LogLevel newLevel)
        {
            Data[path] = Enum.GetName(typeof(LogLevel), newLevel);
            OnReload();
        }
    }
}
