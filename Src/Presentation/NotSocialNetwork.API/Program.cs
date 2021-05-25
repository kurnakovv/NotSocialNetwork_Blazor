using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotSocialNetwork.DBContexts;

namespace NotSocialNetwork.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appDbContext = services.GetRequiredService<AppDbContext>();
                DefaultImagesInit.AddTestImage(appDbContext);
            }

            #region Memory data (Hide if using real database)
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var appDbContext = services.GetRequiredService<AppDbContext>();
            //    TestDataInit.AddTestData(appDbContext);
            //}
            #endregion

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
