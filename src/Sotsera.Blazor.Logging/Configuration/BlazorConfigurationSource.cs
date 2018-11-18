using Microsoft.Extensions.Configuration;
using Sotsera.Blazor.Logging.Logger;

namespace Sotsera.Blazor.Logging.Configuration
{
    internal class BlazorConfigurationSource : IConfigurationSource
    {
        private LogManager LogManager { get; set; }

        public BlazorConfigurationSource(LogManager logManager)
        {
            this.LogManager = logManager;
        }

        public Microsoft.Extensions.Configuration.IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var provider = new BlazorConfigurationProvider();

            LogManager.Providers.Add(provider);

            return provider;
        }
    }
}
