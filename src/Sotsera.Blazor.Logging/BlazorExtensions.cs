using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Logging;
using Sotsera.Blazor.Logging.Logger;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlazorExtensions
    {
        public static IServiceCollection AddBlazorLogger(this IServiceCollection services, Func<LogLevel> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            return AddBlazorLogger(services, configure());
        }

        public static IServiceCollection AddBlazorLogger(this IServiceCollection services, LogLevel minimumLevel = LogLevel.Warning)
        {
            var manager = new LogLevelManager(minimumLevel);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(manager.ConfigurationSection());
                builder.Services.TryAddSingleton<ILoggerProvider>(new LoggerProvider());
                builder.Services.TryAddSingleton<ILogLevelManager>(manager);
            });

            return services;
        }
    }
}