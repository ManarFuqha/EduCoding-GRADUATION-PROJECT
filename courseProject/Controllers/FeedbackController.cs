using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using courseProject.Services.Feedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackServices feedbackServices;

        public FeedbackController(IFeedbackServices feedbackServices)
        {
            this.feedbackServices = feedbackServices;
        }


        /// <summary>
        /// Allows a student to add feedback for an instructor.
        /// </summary>
        [HttpPost("AddInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
     
        public async Task<IActionResult> AddInstructorFeedback(Guid studentId, Guid InstructorId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddInstructorFeedback(studentId, InstructorId, Feedback);
            if (addedFeddback.IsError) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }


        /// <summary>
        /// Allows a student to add feedback for a Course he enrolled in .
        /// </summary>
        [HttpPost("AddCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> AddCourseFeedback(Guid studentId, Guid courseId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddCourseFeedback(studentId, courseId, Feedback);
            if (addedFeddback.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            else if (addedFeddback.FirstError.Type == ErrorOr.ErrorType.Validation) return BadRequest(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }


        /// <summary>
        /// Allows a student to add feedback for a website in general.
        /// </summary>
        [HttpPost("AddGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> AddGeneralFeedback(Guid studentId, FeedbackDTO Feedback)
        {
            var addedFeddback = await feedbackServices.AddGeneralFeedback(studentId, Feedback);
            if (addedFeddback.IsError) return NotFound(new ApiResponce { ErrorMassages = addedFeddback.FirstError.Description });
            return Ok(new ApiResponce { Result = "The feedack is added successfully" });
        }


        /// <summary>
        /// Retrieves all general feedback entries with pagination.
        /// </summary>
        [HttpGet("GetAllGeneralFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllGeneralFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllGeneralFeedback();
            
            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }


        /// <summary>
        /// Retrieves all instructors feedback entries with pagination.
        /// </summary>
        [HttpGet("GetAllInstructorFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllInstructorFeedback(Guid? instructorId , [FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllInstructorFeedback(instructorId);

            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }



        /// <summary>
        /// Retrieves all courses feedback entries with pagination.
        /// </summary>
        [HttpGet("GetAllCourseFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllCourseFeedback(Guid? courseId,[FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllCourseFeedback(courseId);

            return Ok(new ApiResponce { Result = (Pagination<FeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }



        /// <summary>
        /// Retrieves all feedback with all types entries with pagination.
        /// </summary>
        [HttpGet("GetAllFeedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllFeedback([FromQuery] PaginationRequest paginationRequest)
        {
            var getFeedback = await feedbackServices.GetAllFeedback();

            return Ok(new ApiResponce { Result = (Pagination<AllFeedbackForRetriveDTO>.CreateAsync(getFeedback, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }


        /// <summary>
        /// Retrieves feedback information by its ID.
        /// </summary>
        [HttpGet("GetFeedbackById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetFeedbackById(Guid id)
        {

            var getFeedback = await feedbackServices.GetFeedbackById(id);
            if (getFeedback.IsError) return NotFound(new ApiResponce { ErrorMassages = getFeedback.FirstError.Description });
            return Ok(new ApiResponce { Result = getFeedback.Value });
        }
    }
}
