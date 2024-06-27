
namespace courseProject.Services.BackgroundServices
{
  using courseProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;




    public class DailyCheckBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        // Constructor to inject IServiceScopeFactory
        public DailyCheckBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        // Method to execute the background task
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Loop until cancellation is requested
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate the delay until the next occurrence of 12:00 AM
                var now = DateTime.Now;
                var nextMidnight = DateTime.Today.AddDays(1); // Next day at 00:00
                var delay = nextMidnight - now;
                
                // Delay until the next 12:00 AM
                await Task.Delay(delay, stoppingToken);

                // Execute the daily check
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // Retrieve the DbContext from the service scope
                    var dbContext = scope.ServiceProvider.GetRequiredService<projectDbContext>();

                    // Call the method to perform the daily check
                    await DailyCheckAsync(dbContext);
                }
            }
        }

        // Method to perform the daily check
        private async Task DailyCheckAsync(projectDbContext dbContext)
        {
            // Get the current date
            DateTime currentDate = DateTime.Now.Date;

            // Retrieve courses that start on the current date and are not yet started
            var courses = await dbContext.courses
                .Where(c => c.startDate.Value.Date <= currentDate && c.status == "accredit")
                .ToListAsync();

            // Update the status of courses and save changes
            foreach (var course in courses)
            {
                course.status = "start";
                dbContext.Set<Course>().Update(course);
                await dbContext.SaveChangesAsync();
            }
        }
    }

}
    


