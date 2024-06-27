using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class EventRepository : GenericRepository1<Event>, IEventRepository
    {
        private readonly projectDbContext dbContext;

        public EventRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }



        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await dbContext.events
                .Include(x=>x.subAdmin) 
                .FirstOrDefaultAsync(x => x.Id == eventId);
        }


        // get all events created by subAdmin by it's Id , and does not accredit or reject yet
        public async Task<IReadOnlyList<Event>> GetAllUndefindEventBySubAdminIdAsync(Guid subAdminId)
        {
            return await dbContext.events.Include(e => e.subAdmin)
                 .Where(e => e.SubAdminId == subAdminId && e.status.ToLower() == "undefined")
                 .OrderByDescending(x=>x.dateOfAdded)
                 .ToListAsync();
        }

        public async Task CreateEvent(Event model)
        {
            await dbContext.Set<Event>().AddAsync(model);
        }


        public async Task updateEvent(Event model)
        {
            dbContext.Update(model);
        }


        public async Task<IReadOnlyList<Event>> GetAllEventsAsync(string? dateStatus)
        {
            // Check if the generic type T is Event
          
                // Query to get events with status "accredit" and include related SubAdmin and User entities
                var events = dbContext.events
                    .Include(x => x.subAdmin)
                    .Where(x => x.status == "accredit");

                // Filter events based on the dateStatus parameter
                if (dateStatus == "upcoming")
                {
                    // Get events with dates in the future or today
                    events = events.Where(x => x.dateOfEvent.Value.Date >= DateTime.UtcNow.Date);
                }
                else if (dateStatus == "expired")
                {
                    // Get events with dates in the past
                    events = events.Where(x => x.dateOfEvent.Value.Date < DateTime.UtcNow.Date);
                }
                // Return the list of events as IReadOnlyList<T>
                return await events.ToListAsync();
          
        }



        public async Task<IReadOnlyList<Event>> GetAllEventsForAccreditAsync()
        {
            
                return await dbContext.events
                    .Include(x => x.subAdmin).OrderByDescending(x => x.dateOfAdded).ToListAsync();
           
        }

    }
}
