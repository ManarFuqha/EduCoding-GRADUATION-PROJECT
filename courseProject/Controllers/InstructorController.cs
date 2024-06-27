using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models;
using Microsoft.AspNetCore.Mvc;
using courseProject.Services.Instructors;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.LecturesDTO;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IinstructorServices instructorServices;

        public InstructorController(IinstructorServices instructorServices)
        {
            this.instructorServices = instructorServices;
        }


        // <summary>
        /// Allows an instructor to add office hours.
        /// </summary>
        /// <param name="InstructorId">The ID of the instructor adding office hours.</param>
        /// <param name="_Working_Hours">DTO containing the office hours details.</param>
        /// <returns>An IActionResult representing the outcome of adding office hours.</returns>
        [HttpPost("AddInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Instructor")]
        public async Task<IActionResult> AddOfficeHours(Guid InstructorId, [FromForm] WorkingHourDTO _Working_Hours)
        {

           var addHours = await instructorServices.AddOfficeHours(InstructorId, _Working_Hours);
            if (addHours.FirstError.Type == ErrorOr.ErrorType.NotFound)
              return NotFound( new ApiResponce {ErrorMassages=addHours.FirstError.Description });

            else if (addHours.FirstError.Type == ErrorOr.ErrorType.Validation)
                return NotFound(new ApiResponce { ErrorMassages = addHours.FirstError.Description });
            return Ok(new ApiResponce {Result= "The Hours is added successfully" });

        }


        /// <summary>
        /// Retrieves office hours of an instructor by their ID.
        /// </summary>
        /// <param name="Instructorid">The ID of the instructor whose office hours to retrieve.</param>
        [HttpGet("GetInstructorOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetInstructorOfficeHourById(Guid Instructorid)
        {

           var getHours = await instructorServices.GetInstructorOfficeHours(Instructorid);
            if (getHours.IsError) return NotFound(new ApiResponce { ErrorMassages=getHours.FirstError.Description});
            return Ok(new ApiResponce {Result=getHours.Value });

        }



        /// <summary>
        /// Retrieves a list of instructors for a dropdown list.
        /// </summary>
        [HttpGet("GetAllInstructorsList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       
        public async Task<IActionResult> GetInstructorsForDropDownList()
        {
            return Ok(new ApiResponce { Result=await instructorServices.GetAllInstructorsList()});
        }



        /// <summary>
        /// Retrieves all instructors with their respective office hours.
        /// </summary>
        [HttpGet("GetAllInstructorsOfficeHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllIinstructorsWithAllOfficeHours()
        {           
            return Ok(new ApiResponce {Result = await instructorServices.GetAllInstructorsOfficeHours() });
        }



        /// <summary>
        /// Retrieves a list of instructors available for booking lectures based on provided criteria.
        /// </summary>
        /// <param name="lectureForm">DTO containing criteria for selecting instructors.</param>
        /// <returns>An IActionResult representing the list of instructors or appropriate error responses.</returns>
        [HttpGet("GetListOfInstructorForLectures")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Student")]
        public async Task<IActionResult> GetAListOfInstrcutorsForBookALectures([FromQuery]LectureFormDTO lectureForm)
        {
            var getInstructors = await instructorServices.GetListOfInstructorForLectures(lectureForm);
            if (getInstructors.IsError) return Ok(new ApiResponce { ErrorMassages = getInstructors.FirstError.Description });
            return Ok(new ApiResponce { Result = getInstructors.Value });
        }


        /// <summary>
        /// Adds a skill description or biography for an instructor.
        /// </summary>
        [HttpPatch("AddASkillDescription")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddASkillDescriptionForInstructor(Guid instructorId , SkillDescriptionDTO skillDescriptionDTO)
        {
            var addedSkill = await instructorServices.AddSkillDescription(instructorId, skillDescriptionDTO);
            if (addedSkill.IsError) return NotFound(new ApiResponce { ErrorMassages=addedSkill.FirstError.Description});
            return Ok(new ApiResponce { Result = "The description is added successfully" });
        }

        

    }
}
