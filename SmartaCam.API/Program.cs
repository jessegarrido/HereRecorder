using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace SmartaCam.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           // builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<IWavTakeRepository, WavTakeRepository>();
            builder.Services.AddTransient<IMp3TakeRepository, Mp3TakeRepository>();
            builder.Services.AddTransient<IMp3TagSetRepository, Mp3TagSetRepository>();
            builder.Services.AddHostedService<DbInitializerHostedService>();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // app.UseHttpsRedirection();

            //app.UseAuthorization();


          //  app.MapControllers();

            app.Run();
        }
        public class DbInitializerHostedService : IHostedService
        {
            public async Task StartAsync(CancellationToken stoppingToken)
            {
                // The code in here will run when the application starts, and block the startup process until finished
                using (var context = new TakeContext())
                {
                    context.Database.EnsureCreated();
                }

            }

            public Task StopAsync(CancellationToken stoppingToken)
            {
                // The code in here will run when the application stops
                return Task.CompletedTask;
            }
        }
    }
}
