using Gallerio.Web.Options;
using Gallerio.Web.Services.BackendProvider;
using Gallerio.Web.Services.Breadcrumbs;
using Gallerio.Web.Services.LayoutConfiguration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Gallerio.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IBreadcrumbsProvider, BreadcrumbsProvider>();
            builder.Services.AddSingleton<IBackendProvider, BackendProvider>();
            builder.Services.Configure<BackendOptions>(builder.Configuration.GetSection("BackendOptions"));

            builder.Services.AddSingleton<ILayoutConfigurationService, LayoutConfigurationService>();

            await builder.Build().RunAsync();
        }
    }
}