using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using courseProject.Services.Requests;
using courseProject.Core.Models.DTO.StudentsDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestServices requestServices;

        public RequestController(IRequestServices requestServices)
        {
            this.requestServices = requestServices;
        }





        /// <summary>
        /// Retrieves all requests from students to join courses.
        /// </summary>
        /// <param name="paginationRequest">Pagination parameters for paging through results.</param>
        /// <returns>An IActionResult containing paginated list of join course requests or appropriate error responses.</returns>
        [HttpGet("GetAllRequestToJoinCourses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MainSubAdmin")]
        
        public async Task<IActionResult> GetAllRequestFromStudentsToJoinCourses([FromQuery] PaginationRequest paginationRequest)
        {
            
             var getRequests = await requestServices.GetAllRequestToJoinCourses();
            return Ok(new ApiResponce { Result = (Pagination<ViewTheRequestOfJoindCourseDTO>.CreateAsync(getRequests, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });

        }







        /// <summary>
        /// Allows a student to request creation of a custom course.
        /// </summary>
        /// <param name="studentid">The ID of the student making the request.</param>
        /// <param name="studentCustomCourse">DTO containing details of the custom course requested.</param>
        /// <returns>An IActionResult indicating success or failure of the request.</returns>
        [HttpPost("RequestToCreateCustomCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        
        public async Task<IActionResult> RequestToCreateACustomCourse(Guid studentid, [FromForm] StudentCustomCourseDTO studentCustomCourse)
        {
            var customCourse = await requestServices.RequestToCreateCustomCourse(studentid, studentCustomCourse);
            if (customCourse.IsError) return NotFound( new ApiResponce { ErrorMassages = customCourse.FirstError.Description });
            return Ok(new ApiResponce {Result="The Request For Create Custom Course is Sent Successfully" });
        }
    }
}
