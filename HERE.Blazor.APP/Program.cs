using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HERE.Blazor.APP.Services;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace HERE.Blazor.APP
{
	public class Program
	{
		public static async Task Main(string[] args)
		{

			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			string apiUrl = builder.HostEnvironment.BaseAddress.Replace("7140","7152");

			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddHttpClient<IMp3TagSetService, Mp3TagSetService>  (client => client.BaseAddress = new Uri(apiUrl));
			builder.Services.AddHttpClient<ITransportService, TransportService>(client => client.BaseAddress = new Uri(apiUrl));
			builder.Services.AddHttpClient<ITakeService, TakeService>(client => client.BaseAddress = new Uri(apiUrl));
			builder.Services.AddHttpClient<ISettingsService, SettingsService>(client => client.BaseAddress = new Uri(apiUrl));

			await builder.Build().RunAsync();
		}


		//public static async Task Main(string[] args)
		//{
		//	var builder = WebAssemblyHostBuilder.CreateDefault(args);
		//	builder.RootComponents.Add<App>("#app");
		//	builder.RootComponents.Add<HeadOutlet>("head::after");

		//	builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

		//	await builder.Build().RunAsync();
		//}
	}
}
