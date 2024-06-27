using courseProject.Services.BackgroundServices;

namespace courseProject.Configuration
{
    public static class BackgroundServicesDependency
    {

        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<DailyCheckBackgroundService>();

            return services;
        }

    }
}
