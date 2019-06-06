using MasternodePools.Data.Context;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services;
using MasternodePools.Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasternodePools.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMasternodeEntities(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IEntityService<User>, UserService>();
        }
    }
}
