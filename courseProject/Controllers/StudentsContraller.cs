using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Services.Students;
using courseProject.Services.StudentCourses;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsContraller : ControllerBase
    {
  
        private readonly IStudentServices studentServices;
      
        
        

        public StudentsContraller( IStudentServices studentServices)
           
        {
          
            this.studentServices = studentServices;

        }






        /// <summary>
        /// Endpoint to retrieve a paginated list of all students.
        /// </summary>
        /// <param name="paginationRequest">Pagination parameters for specifying page number and page size.</param>
        /// <returns>An IActionResult containing a paginated list of students.</returns>
        [HttpGet("GetAllStudents")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        

        public async Task <IActionResult> GetAllStudentsAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var Students = await studentServices.GetAllStudents();
            return Ok(new ApiResponce { Result= 
                (Pagination<StudentsInformationDto>.CreateAsync(Students, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }








        /// <summary>
        /// Endpoint to retrieve participants of a specific course.
        /// </summary>
        /// <param name="Courseid">The ID of the course.</param>
        /// <param name="paginationRequest">Pagination parameters for specifying page number and page size.</param>
        /// <returns>An IActionResult containing a paginated list of course participants.</returns>
        [HttpGet("GetCourseParticipants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , EnrolledInCourse")]
        
        public async Task<IActionResult> GetCourseParticipants(Guid Courseid, [FromQuery] PaginationRequest paginationRequest)
        {

            var StudentsParticipants = await studentServices.GetCourseParticipants(Courseid);
            if (StudentsParticipants.IsError) return NotFound(new ApiResponce { ErrorMassages= StudentsParticipants .FirstError.Description});
            return Ok(new ApiResponce { Result = 
                (Pagination<StudentsInformationDto>.CreateAsync(StudentsParticipants.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result});             
        }


       


       

    }


    }

