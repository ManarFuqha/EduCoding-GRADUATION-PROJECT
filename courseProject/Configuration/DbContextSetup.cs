using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Configuration
{
    public static class DbContextSetup
    {


        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
          

            services.AddDbContext<projectDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                         options.SerializerSettings.ReferenceLoopHandling =
                                 Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMemoryCache();





            return services;
        }


    }
}
