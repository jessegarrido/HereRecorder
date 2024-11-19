////using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using System.Text.Json.Serialization;



//namespace SmartaCam
//{
//    public class Startup
//    {
//        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
//        {
//            // var services = new ServiceCollection();

//            services.AddTransient<IAudioRepository, AudioRepository>();
//            services.AddTransient<INetworkRepository, NetworkRepository>();
//            services.AddTransient<IUIRepository, UIRepository>();
//            services.AddTransient<IIORepository, IORepository>();
//            services.AddTransient<IWavTakeRepository, WavTakeRepository>();
//            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//            var configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            // .AddJsonFile("appsettings.json")
//            // .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
//            .Build();

//            Log.Logger = new LoggerConfiguration()
//                .ReadFrom.Configuration(configuration)
//                .CreateLogger();
//        }
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {


//                SetupLogging.Development();


//            }
//            else
//            {
//                SetupLogging.Production();
//            }

//            app.UseRouting();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }

//    }
//}
