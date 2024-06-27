using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Services.StudentCourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCoursesServices studentCoursesServices;

        public StudentCourseController(IStudentCoursesServices studentCoursesServices)
        {
            this.studentCoursesServices = studentCoursesServices;
        }








        /// <summary>
        /// Endpoint for a student to enroll in a course.
        /// </summary>
        /// <param name="studentCourseDTO">Data required to enroll in the course.</param>
        /// <returns>An IActionResult indicating success or failure of enrolling in the course.</returns>
        [HttpPost("EnrollInCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        public async Task<IActionResult> EnrollInCourseAsync(StudentCourseDTO studentCourseDTO)
        {
            var enrolledCourse = await studentCoursesServices.EnrollInCourse(studentCourseDTO);
            if (enrolledCourse.FirstError.Type == ErrorOr.ErrorType.NotFound)
                return NotFound(new ApiResponce { ErrorMassages = enrolledCourse.FirstError.Description });
            else if (enrolledCourse.FirstError.Type == ErrorOr.ErrorType.Validation)
                return Ok(new ApiResponce { ErrorMassages = enrolledCourse.FirstError.Description });

            return Ok(new ApiResponce { Result = "The Request To Join Course Is Created Successfully" });
        }









        /// <summary>
        /// Endpoint for a main sub-admin to approve or reject a student's request to join a course.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="status">The status to set for the approval ('approved' or 'rejected').</param>
        /// <returns>An IActionResult indicating success or failure of updating the approval status.</returns>
        [HttpPatch("ApprovelToJoin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MainSubAdmin")]
        public async Task<IActionResult> ApprovalForTheStudentToJoinTheCourse(Guid courseId, Guid studentId, string status)
        {
            var editedStatus = await studentCoursesServices.ApprovelToJoinCourse(courseId, studentId, status);
            if (editedStatus.IsError) return NotFound(new ApiResponce { ErrorMassages = editedStatus.FirstError.Description });
            return Ok(new ApiResponce { Result= "Status updated successfully." });
        }
    }
}
