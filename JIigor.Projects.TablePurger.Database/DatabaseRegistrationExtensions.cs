using JIigor.Projects.TablePurger.Database.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIigor.Projects.TablePurger.Database
{
    public static class DatabaseRegistrationExtensions
    {
        public static IServiceCollection AddPurgerDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.Bind("SqlServerConfiguration", databaseConfiguration);

            return services.AddDbContext<PurgerDataContext>(options =>
            {
                options.UseSqlServer(databaseConfiguration.ConnectionString);
            }, ServiceLifetime.Singleton);
        } 
    }
}
