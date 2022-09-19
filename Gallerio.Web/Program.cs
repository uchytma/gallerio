using Gallerio.Web.Client;
using Gallerio.Web.Client.Options;
using Gallerio.Web.Client.Services.BackendProvider;
using Gallerio.Web.Client.Services.Breadcrumbs;
using Gallerio.Web.Client.Services.LayoutConfiguration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Gallerio.Web.Client
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