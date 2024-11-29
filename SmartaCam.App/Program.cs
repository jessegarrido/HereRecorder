using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartaCam.App.Services;
using SmartaCam;
using Microsoft.AspNetCore.Components;
using static Dropbox.Api.TeamLog.EventCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SmartaCam.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IMp3TagSetService, Mp3TagSetService>(client => client.BaseAddress = new Uri("https://localhost:7152/"));
            builder.Services.AddHttpClient<ITransportService, TransportService>(client => client.BaseAddress = new Uri("https://localhost:7152/"));
            builder.Services.AddHttpClient<ITakeService, TakeService>(client => client.BaseAddress = new Uri("https://localhost:7152/"));

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }

    }
}
