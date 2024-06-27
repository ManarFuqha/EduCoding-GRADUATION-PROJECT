using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EventsDTO;
using ErrorOr;

namespace courseProject.Services.Events
{
    public interface IEventServices
    {
        public Task<ErrorOr<Created>> CreateEvent(Event _event, Request request);
        public Task<ErrorOr<Updated>> accreditEvent(Guid eventId , string Status);
        public Task<IReadOnlyList<EventDto>> GetAllAccreditEvents(string? dateStatus);
        public Task<IReadOnlyList<EventAccreditDto>> GetAllEventsToAccreditByAdmin();
        public Task<IReadOnlyList<EventAccreditDto>> GetAllUndefinedEvents(Guid subAdminId);
        public Task<ErrorOr<Updated>> EditEvent(Guid eventId , EventForEditDTO eventForEditDTO);
        public Task<ErrorOr<EventDto>> GetEventById(Guid eventId);
    }
}
