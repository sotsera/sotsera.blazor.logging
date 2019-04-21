using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var url = GetUrl();

            services.AddBlazorLogger(() => url.StartsWith("http://localhost") ? LogLevel.Trace : LogLevel.Warning);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }

        private string GetUrl()
        {
            //var serviceProvider = new BrowserServiceProvider(configure => { });

            //return BrowserUriHelper.Instance.GetBaseUri();
            return "http://localhost";
        }
    }
}