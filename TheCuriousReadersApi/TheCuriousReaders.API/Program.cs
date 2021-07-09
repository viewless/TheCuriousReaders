using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TheCuriousReaders.API
{
    public class Program
    {
        public static async Task Main(string[] args)
            {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var serviceProvider = services.GetService<IServiceProvider>();
                    var dbContext = services.GetRequiredService<CuriousReadersContext>();

                    await dbContext.Database.MigrateAsync();
                    await CuriousReadersInitializer.Initialize(dbContext, serviceProvider);
                }
                catch (Exception e)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured while initializing the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
