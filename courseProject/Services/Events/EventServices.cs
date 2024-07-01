using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;


namespace courseProject.Services.Events
{
    public class EventServices : IEventServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EventServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       

        public async Task<ErrorOr<Created>> CreateEvent(Event _event, Request request)
        {
            // Check if the SubAdmin exists
            var SubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(_event.SubAdminId, "subadmin");          
            var MainSubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(_event.SubAdminId, "main-subadmin");
            if (SubAdminFound == null && MainSubAdminFound == null) return ErrorSubAdmin.NotFound;
    

            // Upload event image if provided
            if (_event.image != null)
            {
                _event.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(_event.image);
            }

            // Begin a transaction
            using (var transaction = await unitOfWork.UserRepository.BeginTransactionAsync())
            {

                

                // Create the event
                await unitOfWork.eventRepository.CreateEvent(_event);
                var success2 = await unitOfWork.StudentRepository.saveAsync();

                // Commit the transaction if both operations are successful
                if ( success2 > 0)
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                // Return an error if any operation fails
                return ErrorEvent.hasError;

            }
        }


        //change the status of event from undefined to accredit or reject
        public async Task<ErrorOr<Updated>> accreditEvent(Guid eventId, string Status)
        {
            // Get the event by ID
            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(eventId);
            if (getEvent == null) return ErrorEvent.NotFound;

            // Define the property to update and create a JsonPatchDocument to replace the value
            Expression<Func<Course, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<Course>();
            patchDocument.Replace(path, Status);

            // Update the event status
            getEvent.status = Status;

            // Update the event in the repository
            await unitOfWork.eventRepository.updateEvent(getEvent);
            // Save changes to the database
            await unitOfWork.UserRepository.saveAsync();
            // Return a success message
            return Result.Updated;
        }


        // Retrieves all accredited events based on the provided date status.
        public async Task<IReadOnlyList<EventDto>> GetAllAccreditEvents(string? dateStatus)
        {
            var events = await unitOfWork.eventRepository.GetAllEventsAsync(dateStatus);

            // Order events by date of addition
            events = events.OrderByDescending(x => x.dateOfAdded).ToList();

            // Update image URLs if they exist
            foreach (var _event in events)
            {
                if (_event.ImageUrl != null)
                {
                    _event.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(_event.ImageUrl);
                }
            }
            var mapperEvent = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDto>>(events);
            return mapperEvent;
        }


        // Retrieves all events for accreditation by an admin.
        public async Task<IReadOnlyList<EventAccreditDto>> GetAllEventsToAccreditByAdmin()
        {
            var events = await unitOfWork.eventRepository.GetAllEventsForAccreditAsync();
            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            return mapperEvents;
        }

        public async Task<IReadOnlyList<EventAccreditDto>> GetAllUndefinedEvents(Guid subAdminId)
        {
            var events = await unitOfWork.eventRepository.GetAllUndefindEventBySubAdminIdAsync(subAdminId);
            var mapperEvents = mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventAccreditDto>>(events);
            return mapperEvents;
        }

        public async Task<ErrorOr<Updated>> EditEvent(Guid eventId, EventForEditDTO eventForEditDTO)
        {

            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(eventId);
            if (getEvent == null) return ErrorEvent.NotFound;

            mapper.Map(eventForEditDTO, getEvent);
            if (eventForEditDTO.image != null)
            {
                getEvent.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(eventForEditDTO.image);
            }
            await unitOfWork.eventRepository.updateEvent(getEvent);
            await unitOfWork.CourseRepository.saveAsync();
            return Result.Updated;

        }


       // Retrieves an event by its ID .
        public async Task<ErrorOr<EventDto>> GetEventById(Guid eventId)
        {
            var getEvent = await unitOfWork.eventRepository.GetEventByIdAsync(eventId);
            if(getEvent==null) return ErrorEvent.NotFound;

            var eventMapper = mapper.Map<Event , EventDto>(getEvent);
            if (eventMapper.ImageUrl != null)
            {
                eventMapper.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(eventMapper.ImageUrl);
            }
            return eventMapper;
        }
    }
}
