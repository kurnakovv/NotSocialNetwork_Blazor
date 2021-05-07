using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NotSocialNetwork.DBContexts
{
    public static class SetupDbContextInStartup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

        public static void AddMemoryDbContext(this IServiceCollection services, string databaseName)
            => services.AddDbContext<AppDbContext>(options
                => options.UseInMemoryDatabase(databaseName));
    }
}
