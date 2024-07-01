using courseProject.Core.IGenericRepository;
using courseProject.Repository.GenericRepository;

namespace courseProject.Emails
{
    public static class Infrastructure
    {

        public static IServiceCollection AddEmailInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {

            var emailConfig = new EmailConfiguration();
            configuration.Bind(nameof(EmailConfiguration), emailConfig);

            // Now configure the JwtSettings class to be injected into the Dependency Injection container
            var emailSection = configuration.GetSection(nameof(EmailConfiguration));
            services.Configure<EmailConfiguration>(emailSection);
           
            //// Add the email service to the DI container
            services.AddTransient<IEmailService, EmailService>();

            return services;

        }
    }
}
