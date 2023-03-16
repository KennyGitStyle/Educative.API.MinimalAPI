using Educative.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Educative.Minimal.API.Extension
{
    public static class DbExtension
    {
        public static IServiceCollection AddDbContextExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EducativeContext>(options =>
            {
                options
                .UseSqlite(config.GetConnectionString("EducativeDefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });

            return services;
        }
    }
}