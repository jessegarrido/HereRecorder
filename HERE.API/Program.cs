using Microsoft.AspNetCore.HttpOverrides;

namespace HERE.API
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<IAudioRepository, AudioRepository>();
            builder.Services.AddTransient<ITakeRepository, TakeRepository>();
            builder.Services.AddTransient<IMp3TagSetRepository, Mp3TagSetRepository>();
            builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();
            builder.Services.AddHostedService<DbInitializerHostedService>();
			// builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
			//builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

			builder.Services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
            });

            var app = builder.Build();
          //  app.UseHttpsRedirection();
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				app.UseSwagger();
                app.UseSwaggerUI();
			}
            
            app.UseRouting();

			app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
		    .AllowAnyOrigin()
         	);



			//    app.UseAuthorization();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapGet("/", () => "Hello ForwardedHeadersOptions!");

            UIRepository uIRepository = new();
            _ = Task.Run(async () => { await uIRepository.SessionInitAsync(); });

            
            app.MapControllers();

            app.Run();
        }
        
        public class DbInitializerHostedService : IHostedService
        {
            public async Task StartAsync(CancellationToken stoppingToken)
            {
				// The code in here will run when the application starts, and block the startup process until finished
				var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "HERE");
                
                if (!Directory.Exists(dbPath))
                {
                     UIRepository uIRepository = new();
                     string os = await uIRepository.IdentifyOS();
                    Console.WriteLine($"Creating dbPath: {dbPath}");
					DirectoryInfo di = Directory.CreateDirectory(dbPath);
					if (os != "Windows")
					{
						File.SetUnixFileMode(dbPath, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
					}
                }
                using (var context = new HEREContext())
                {
                    await context.Database.EnsureCreatedAsync();
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
