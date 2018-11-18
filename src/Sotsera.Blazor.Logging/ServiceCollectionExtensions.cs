using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Logging;
using Sotsera.Blazor.Logging.Logger;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorLogger(this IServiceCollection services, Func<LogLevel> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            return AddBlazorLogger(services, configure());
        }

        public static IServiceCollection AddBlazorLogger(this IServiceCollection services, LogLevel minimumLevel = LogLevel.Warning)
        {
            var manager = new LogManager(minimumLevel);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(manager.ConfigurationSection());
                builder.Services.TryAddSingleton<ILoggerProvider, LoggerProvider>();
                builder.Services.TryAddSingleton<ILogManager>(manager);
                builder.Services.TryAddSingleton(manager);
            });

            return services;
        }
    }
}