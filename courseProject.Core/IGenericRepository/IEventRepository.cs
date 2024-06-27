using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IEventRepository :IGenericRepository1<Event>
    {
        public Task<Event> GetEventByIdAsync(Guid eventId);
        public Task<IReadOnlyList<Event>> GetAllUndefindEventBySubAdminIdAsync(Guid subAdminId);
        public Task CreateEvent(Event model);
        public Task updateEvent(Event model);
        public Task<IReadOnlyList<Event>> GetAllEventsAsync(string? dateStatus);
        public Task<IReadOnlyList<Event>> GetAllEventsForAccreditAsync();
    }
}
