using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Logging;

namespace Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddBlazorLogger(() =>
                {
                    return BrowserUriHelper.Instance.GetBaseUri().StartsWith("http://localhost")
                        ? LogLevel.Debug
                        : LogLevel.Warning;
                });
            });

            var logManager = serviceProvider.GetService<ILogLevelManager>();
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogDebug($"Blazor log level: {logManager.CurrentLevel}");


            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
