using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using courseProject.Services.Lectures;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureServices lectureServices;

        public LecturesController( ILectureServices lectureServices)
        {
            this.lectureServices = lectureServices;
        }



        /// <summary>
        /// Retrieves all lecture requests associated with an instructor.
        /// </summary>
        /// <param name="instructorId">The ID of the instructor.</param>
        /// <param name="paginationRequest">Pagination parameters for controlling page size and number.</param>
        /// <returns>An IActionResult representing the list of lecture requests or appropriate error responses.</returns>
        [HttpGet("GetAllLectureRequest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [Authorize(Policy = "Main-SubAdmin ,Instructor , Student")]
        
        public async Task<IActionResult> GetAllLecturesByInstructorId(Guid instructorId, [FromQuery] PaginationRequest paginationRequest)
        {

            var GetLectures = await lectureServices.GetAllLecturesByInstructorId(instructorId);
            if (GetLectures.IsError) return NotFound(new ApiResponce { ErrorMassages=GetLectures.FirstError.Description});
            return Ok(new ApiResponce { Result = (Pagination<LecturesForRetriveDTO>.CreateAsync(GetLectures.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
            

        }


        /// <summary>
        /// Books a lecture by a student.
        /// </summary>
        /// <param name="studentId">The ID of the student booking the lecture.</param>
        /// <param name="date">The date of the lecture.</param>
        /// <param name="startTime">The start time of the lecture.</param>
        /// <param name="endTime">The end time of the lecture.</param>
        /// <param name="bookALecture">DTO containing additional details for booking the lecture.</param>
        /// <returns>An IActionResult representing the outcome of booking the lecture or appropriate error responses.</returns>
        [HttpPost("BookALecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        
        public async Task<IActionResult> BooKLectureByStudent(Guid studentId, DateTime date, string startTime, string endTime, [FromForm] BookALectureDTO bookALecture)
        {
            var lecture = await lectureServices.BookALecture(studentId , date, startTime, endTime, bookALecture);
            if (lecture.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce {ErrorMassages=lecture.FirstError.Description });
            else if (lecture.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = lecture.FirstError.Description });
            return Ok(new ApiResponce { Result = "The Lecture is Created Successfully" });
        }



        /// <summary>
        /// Allows a student to join a public lecture.
        /// </summary>
        /// <param name="StudentId">The ID of the student joining the lecture.</param>
        /// <param name="ConsultaionId">The ID of the public lecture.</param>
        /// <returns>An IActionResult representing the outcome of joining the lecture or appropriate error responses.</returns>
        [HttpPost("JoinToPublicLecture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Student")]
        
        public async Task<IActionResult> JoinToAPublicLecture(Guid StudentId, Guid ConsultaionId)
        {
            var JoinedLecture = await lectureServices.JoinToPublicLecture(StudentId, ConsultaionId);
            if (JoinedLecture.IsError) return NotFound(new ApiResponce {ErrorMassages=JoinedLecture.FirstError.Description });
            return Ok(new ApiResponce { Result= "Joined successfully" });
        }



        /// <summary>
        /// Retrieves all consultations associated with a student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="paginationRequest">Pagination parameters for controlling page size and number.</param>
        /// <returns>An IActionResult representing the list of consultations or appropriate error responses.</returns>
        [HttpGet("GetAllConsultations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetAllConsultation(Guid studentId, [FromQuery] PaginationRequest paginationRequest)
        {
            var getLectures = await lectureServices.GetAllConsultations(studentId);
            if (getLectures.IsError) return NotFound(new ApiResponce { ErrorMassages=getLectures.FirstError.Description });         
            return Ok(new ApiResponce { Result= (Pagination<PublicLectureForRetriveDTO>.CreateAsync(getLectures.Value, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });
        }



        /// <summary>
        /// Retrieves a consultation by its ID.
        /// </summary>
        [HttpGet("GetConsultationById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetAConsultationById(Guid consultationId)
        {

            var getConsultation = await lectureServices.GetConsultationById(consultationId);
            if(getConsultation.IsError)  return NotFound(new ApiResponce {ErrorMassages=getConsultation.FirstError.Description });
            return Ok(new ApiResponce {Result=getConsultation.Value });
        }
    }
}
