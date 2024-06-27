using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.EventsDTO;
using courseProject.Services.Events;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventContraller : ControllerBase
    {
       
        private readonly IMapper mapper;
        private readonly IEventServices eventServices;
       

        public EventContraller( IMapper mapper , IEventServices eventServices)
        {
          
            this.mapper = mapper;
            this.eventServices = eventServices;
          
        }


        /// <summary>
        /// HTTP GET endpoint to retrieve all accredited events with optional date filtering and pagination.
        /// </summary>
        /// <param name="dateStatus">Optional parameter to filter events by date status (e.g., "upcoming", "expired").</param>
        /// <param name="paginationRequest">Pagination request parameters including page number and page size.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce with a paginated list of accredited events.
        /// Possible HTTP status codes:
        /// 200 - Successful response with the list of events.
        /// 404 - Not found response (not applicable in current implementation but can be used for future enhancements).
        /// 400 - Bad request response.
        /// </returns>
        [HttpGet("GetAllAccreditEvents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        
        public async Task<ActionResult< ApiResponce>> GetAllAccreditEventsAsync(string? dateStatus ,[FromQuery] PaginationRequest paginationRequest)
        {
            // Retrieve the list of accredited events with optional date filtering
            var events = await eventServices.GetAllAccreditEvents(dateStatus);

            // Retrieve the list of accredited events with optional date filtering
            // Return the paginated events wrapped in an ApiResponce object with a 200 OK status
            return Ok(new ApiResponce { Result = (Pagination<EventDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }




        /// <summary>
        /// HTTP GET endpoint for admin users to retrieve all events that need to be accredited with pagination support.
        /// </summary>
        /// <param name="paginationRequest">Pagination request parameters including page number and page size.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce with a paginated list of events to be accredited.
        /// Possible HTTP status codes:
        /// 200 - Successful response with the list of events.
        /// 404 - Not found response (not applicable in current implementation but can be used for future enhancements).
        /// 400 - Bad request response.
        /// </returns>
        [HttpGet("GetAllEventsToAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        
        public async Task<ActionResult<ApiResponce>> GetAllEventsToAccreditAsync([FromQuery] PaginationRequest paginationRequest)
        {
            // Retrieve the list of events to be accredited by admin
            var events = await eventServices.GetAllEventsToAccreditByAdmin();

            // Create a paginated response for the events
            // Return the paginated events wrapped in an ApiResponce object with a 200 OK status
            return Ok( new ApiResponce { Result = (Pagination<EventAccreditDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
            
        }





        /// <summary>
        /// HTTP GET endpoint for Main-SubAdmin and SubAdmin users to retrieve all undefined events assigned to a specific his Id with pagination support.
        /// </summary>
        /// <param name="subAdminId">The ID of the SubAdmin or Main-Subadmin for whom the undefined events are to be retrieved.</param>
        /// <param name="paginationRequest">Pagination request parameters including page number and page size.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce with a paginated list of undefined events.
        /// Possible HTTP status codes:
        /// 200 - Successful response with the list of events.
        /// 404 - Not found response (not applicable in current implementation but can be used for future enhancements).
        /// 400 - Bad request response.
        /// </returns>
        [HttpGet("GetAllUndefinedEventsToSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Main-SubAdmin , SubAdmin")]
        
        public async Task<ActionResult<ApiResponce>> GetAllEvents(Guid subAdminId, [FromQuery] PaginationRequest paginationRequest)
        {
            // Retrieve the list of undefined events assigned to the specified SubAdmin /Main-SubAdmin
            var events = await eventServices.GetAllUndefinedEvents(subAdminId);

            // Create a paginated response for the events
            // Return the paginated events wrapped in an ApiResponce object with a 200 OK status
            return Ok(new ApiResponce { Result = (Pagination<EventAccreditDto>.CreateAsync(events, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }





        /// <summary>
        /// HTTP POST endpoint for SubAdmin and Main-SubAdmin users to create a new event.
        /// </summary>
        /// <param name="model">The EventForCreateDTO object containing the event details to be created.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce.
        /// Possible HTTP status codes:
        /// 200 - Successful response indicating the event is created (though 201 Created is more appropriate here).
        /// 404 - Not found response if the input values are null or if there was an error creating the event.
        /// 400 - Bad request response if the model state is invalid or other validation errors occur.
        /// 201 - Indicates that the event was created successfully.
        /// </returns>
        [HttpPost("CreateEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "SubAdmin , Main-SubAdmin")]
        public async Task<ActionResult<ApiResponce>> createEvent([FromForm] EventForCreateDTO model)
        {

            // Map the model to Event and Request objects
            var EventMapped = mapper.Map<Event>(model);
            var requestMapped = mapper.Map<Request>(model);

            // Create the event using the event service
            var createCourse = await eventServices.CreateEvent(EventMapped, requestMapped);

            if (createCourse.IsError) return NotFound(new ApiResponce { ErrorMassages =  createCourse.FirstError.Description  });

            // Return a successful response indicating the event was created
            return new ApiResponce { Result = "The Event Is Created Successfully" };
            
            }





        /// <summary>
        /// HTTP PATCH endpoint for admin users to update the status of an event.(accredit or reject)
        /// </summary>
        /// <param name="eventId">The ID of the event to update.</param>
        /// <param name="eventStatus">The EventStatusDTO object containing the new status of the event.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce.
        /// Possible HTTP status codes:
        /// 200 - Successful response indicating the event status was updated.
        /// 404 - Not found response if the event or status update operation failed.
        /// 400 - Bad request response if the input is invalid or other validation errors occur.
        /// </returns>
        [HttpPatch("accreditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ApiResponce>> EditEventStatus(Guid eventId, EventStatusDTO eventStatus)
        {
            // Update the event status using the event service
            var updateStatus = await eventServices.accreditEvent(eventId ,eventStatus.Status);
            if (updateStatus.IsError) return NotFound(new ApiResponce
            {
                ErrorMassages =  updateStatus.FirstError.Description 
            });

            // Return a successful response indicating the event status was updated
            return Ok(new ApiResponce
            {
                Result = $"The Event is {eventStatus.Status}"
            });
        }





        /// <summary>
        /// HTTP PUT endpoint for Admin, Main-SubAdmin, and SubAdmin users to edit an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to edit.</param>
        /// <param name="eventForEditDTO">The EventForEditDTO object containing the updated event details.</param>
        /// <returns>
        /// Returns an ActionResult containing an ApiResponce.
        /// Possible HTTP status codes:
        /// 200 - Successful response indicating the event was updated successfully.
        /// 404 - Not found response if the event does not exist or the update operation failed.
        /// 400 - Bad request response if the input is invalid or other validation errors occur.
        /// </returns>
        [HttpPut("EditEvent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin, Main-SubAdmin , SubAdmin")]
      
        public async Task<ActionResult<ApiResponce>> EditEvent(Guid id, [FromForm] EventForEditDTO eventForEditDTO)
        {

            
            var editEvent = await eventServices.EditEvent(id, eventForEditDTO);
            if (editEvent.IsError) return NotFound(new ApiResponce { ErrorMassages=editEvent.FirstError.Description});
            return Ok(new ApiResponce { Result="The event is updated successfully"});                                                               
        }




        /// <summary>
        /// Retrieves an event by its ID and returns it as an API response.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the event data if found, or an error message if the event is not found.
        /// </returns>
        /// <response code="200">Returns the event data.</response>
        /// <response code="404">If the event is not found.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpGet("GetEventById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            // Call the service to get the event by ID
            var getEvent = await eventServices.GetEventById(eventId);

            // If an error occurred (event not found), return 404 with an error message
            if (getEvent.IsError) return NotFound(new ApiResponce { ErrorMassages=getEvent.FirstError.Description});

            // If the event is found, return 200 with the event data
            return Ok(new ApiResponce { Result=getEvent.Value});
        }


    }
}
