using courseProject.Common;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace courseProject.Configuration
{
    public static class FluentValidationDependancy
    {

        public static IServiceCollection FluentValidation(this IServiceCollection services)
        {

            services.AddFluentValidation();
            services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

            return services;
        }



    }
}
