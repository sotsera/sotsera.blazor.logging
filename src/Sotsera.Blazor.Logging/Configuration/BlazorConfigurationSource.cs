using Microsoft.Extensions.Configuration;
using Sotsera.Blazor.Logging.Logger;

namespace Sotsera.Blazor.Logging.Configuration
{
    internal class BlazorConfigurationSource : IConfigurationSource
    {
        private LogLevelManager LogLevelManager { get; set; }

        public BlazorConfigurationSource(LogLevelManager logLevelManager)
        {
            this.LogLevelManager = logLevelManager;
        }

        public Microsoft.Extensions.Configuration.IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var provider = new BlazorConfigurationProvider();

            LogLevelManager.Providers.Add(provider);

            return provider;
        }
    }
}
