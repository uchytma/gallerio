using Gallerio.Web.Client;
using Gallerio.Web.Client.Services.Breadcrumbs;
using Gallerio.Web.Client.Services.LayoutConfiguration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
            builder.Services.AddSingleton<ILayoutConfigurationService, LayoutConfigurationService>();

            await builder.Build().RunAsync();
        }
    }
}