using Microsoft.AspNetCore.Cors.Infrastructure;

namespace courseProject.Configuration
{
    public static class CorsPolicySetup
    {

        /// <summary>
        /// Configures CORS policy named "AllowOrigin" to allow requests from "http://localhost:3000".
        /// </summary>
        /// <param name="policyBuilder">The policy builder to configure CORS.</param>
        public static void ConfigureCorsPolicy(CorsPolicyBuilder policyBuilder)
        {
            
                policyBuilder.WithOrigins("http://localhost:3000")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials();
            

        }


        }
}
