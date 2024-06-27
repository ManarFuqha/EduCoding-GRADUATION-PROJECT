using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using courseProject.Services.Submissions;
using courseProject.Core.Models.DTO.MaterialsDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionServices submissionServices;

        public SubmissionsController( ISubmissionServices submissionServices)
        {
            this.submissionServices = submissionServices;
        }







        /// <summary>
        /// Endpoint to retrieve all submissions for a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task for which submissions are requested.</param>
        /// <param name="paginationRequest">Pagination parameters for specifying page number and page size.</param>
        /// <returns>An IActionResult containing a paginated list of submissions.</returns>
        [HttpGet("GetAllSubmissionForTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        
        public async Task<IActionResult> GetAllSubmissionUsingTaskId(Guid taskId, [FromQuery] PaginationRequest paginationRequest)
        {
            var getSubmissions = await submissionServices.GetAllSubmissionForTask(taskId);
            if (getSubmissions.IsError) return NotFound(new ApiResponce { ErrorMassages=getSubmissions.FirstError.Description});
            return Ok(new ApiResponce { Result = (Pagination<StudentSubmissionDTO>.CreateAsync(getSubmissions.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }








        /// <summary>
        /// Endpoint for a student to add a submission for a task.
        /// </summary>
        /// <param name="Studentid">The ID of the student submitting the task.</param>
        /// <param name="Id">The ID of the task being submitted.</param>
        /// <param name="submissions">DTO containing submission details.</param>
        /// <returns>An IActionResult indicating success or failure of the submission.</returns>
        [HttpPost("AddTaskSubmission")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MaterialInEnrolledCourseForStudent")]
        public async Task<IActionResult> AddTaskByStudent(Guid Studentid, Guid Id, [FromForm] SubmissionsDTO submissions)
        {
            var addedTask = await submissionServices.AddTaskSubmission(Studentid, Id, submissions);
            if (addedTask.IsError) return NotFound(new ApiResponce { ErrorMassages = addedTask.FirstError.Description });
            return Ok(new ApiResponce {Result="The Task Is Added Successfully" });
        }
    }
}
