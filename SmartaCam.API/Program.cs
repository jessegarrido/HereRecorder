using System.Text.Json.Serialization;
using static SmartaCam.IAudioRepository;

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
            builder.Services.AddTransient<IAudioRepository, AudioRepository>();
            builder.Services.AddTransient<ITakeRepository, TakeRepository>();
           // builder.Services.AddTransient<IMp3TakeRepository, Mp3TakeRepository>();
            builder.Services.AddTransient<IMp3TagSetRepository, Mp3TagSetRepository>();
            builder.Services.AddHostedService<DbInitializerHostedService>();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           //  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            UIRepository uIRepository = new();
            _ = Task.Run(async () => { await uIRepository.SessionInit(); });

             app.UseHttpsRedirection();




              app.MapControllers();

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
