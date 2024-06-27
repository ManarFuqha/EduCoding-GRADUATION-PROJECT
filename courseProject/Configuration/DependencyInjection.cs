using courseProject.Emails;

namespace courseProject.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            

            services.AddApplication();

            services. AddInfrastucture( configuration);

            services.AddAuthenticationAndAuthorization(configuration);

            services.AddServices();

            services.FluentValidation();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", CorsPolicySetup.ConfigureCorsPolicy);
            });


            services.AddBackgroundServices();

            services.AddEmailInfrastucture(configuration);



            return services;
        }

    }
}
