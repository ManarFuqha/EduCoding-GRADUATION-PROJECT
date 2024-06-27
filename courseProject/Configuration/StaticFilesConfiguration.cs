using Microsoft.Extensions.FileProviders;

namespace courseProject.Configuration
{
    public static class StaticFilesConfiguration
    {

        public static IApplicationBuilder ConfigureStaticFiles(this IApplicationBuilder app)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

            // Create directory if it does not exist
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // Serve static files from the "Files" directory
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(filePath),
                RequestPath = "/Files"
            });


            return app;
        }

    }
}
