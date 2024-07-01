using courseProject.Core.Models;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Repository.GenericRepository;
using courseProject.Services.Skill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
       
        private readonly ISkillsServices skillsServices;
        private readonly ApiResponce response;
        

        public SkillController(ISkillsServices skillsServices)
        {
          
            this.skillsServices = skillsServices;
            response=new ApiResponce();
           
        }









        /// <summary>
        /// Endpoint for an admin to add a new skill option.
        /// </summary>
        /// <param name="skillName">The name of the skill to add.</param>
        /// <returns>An IActionResult indicating success or failure of adding the skill.</returns>
        [HttpPost("AddSkillOptionsByAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddSkillOptionsByAdmin(string skillName)
        {
            var addSkill = await skillsServices.AddSkillByAdmin(skillName);
            response.Result = addSkill.Value;
            if (addSkill.IsError == true) response.ErrorMassages=addSkill.FirstError.Description;
            return Ok( response);
        }










        /// <summary>
        /// Endpoint to get all skill options .
        /// </summary>
        /// <returns>An IActionResult containing a list of all skill options.</returns>
        [HttpGet("GetAllSkillOptions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Student")]
        public async Task<IActionResult> GetAllOptions([FromQuery] PaginationRequest paginationRequest)
        {
            var skills = await skillsServices.getAllSkillesAddedByAdmin();
            return Ok( new ApiResponce { Result =  
            (Pagination<Skills>.CreateAsync(skills, paginationRequest.pageNumber, paginationRequest.pageSize)).Result}); 
        }









        /// <summary>
        /// Endpoint to get all skill options specifically for an instructor's dropdown, Which he has not chosen yet.
        /// </summary>
        /// <param name="instructorId">The ID of the instructor.</param>
        /// <returns>An IActionResult containing skill options for the specified instructor.</returns>
        [HttpGet("GetAllSkillOptionsToInstructor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize("Instructor")]
        public async Task<IActionResult> GetAllOptionsToInstructorDropdown(Guid instructorId)
        {
            var getSkills = await skillsServices.getAllSkillOptionsToInstructor(instructorId);
            if(getSkills.IsError ) return NotFound(new ApiResponce { ErrorMassages = getSkills.FirstError.Description });
            return Ok( new ApiResponce { Result= getSkills.Value });

        }








        /// <summary>
        /// Endpoint for an instructor to select new skills.
        /// </summary>
        /// <param name="instructorId">The ID of the instructor.</param>
        /// <param name="array">List of skill IDs selected by the instructor.</param>
        /// <returns>An IActionResult indicating success or failure of selecting skills.</returns>
        [HttpPost("selectAnInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> SelectASkillsByInstructor(Guid instructorId, [FromForm] ListIntegerDTO array)
        {
            
           var selectNewSkill = await skillsServices.chooseANewSkillToInstructor(instructorId, array);
            if (selectNewSkill.FirstError.Type == ErrorOr.ErrorType.NotFound)
            {
                return NotFound(new ApiResponce { ErrorMassages = (selectNewSkill.FirstError.Description) });

            }
            else if((selectNewSkill.FirstError.Type == ErrorOr.ErrorType.Validation))
                return Ok(new ApiResponce { ErrorMassages = (selectNewSkill.FirstError.Description) });
            return Ok( new ApiResponce { Result= "The skills is added successfully"});

        }









        /// <summary>
        /// Endpoint for an instructor to delete a selected skill.
        /// </summary>
        /// <param name="InstructorId">The ID of the instructor.</param>
        /// <param name="SkillId">The ID of the skill to delete.</param>
        /// <returns>An IActionResult indicating success or failure of deleting the skill.</returns>
        [HttpDelete("DeleteAnInstructorSkill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<ActionResult<ApiResponce>> DeleteAnInstructorSkillFromSelected(Guid InstructorId, Guid SkillId)
        {
            var deletedSkill = await skillsServices.DeleteAnInstructorSkill(InstructorId, SkillId);
            if (deletedSkill.IsError) return new ApiResponce { ErrorMassages =  deletedSkill.FirstError.Description  };
            return new ApiResponce { Result = "The skill is deleted successfully" };
        }








        /// <summary>
        /// Endpoint to get all skills associated with a specific instructor.
        /// </summary>
        /// <param name="instructorId">The ID of the instructor.</param>
        /// <returns>An IActionResult containing a list of skills for the instructor.</returns>
        [HttpGet("GetAllInstructorSkills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllInstructorSkills(Guid instructorId)
        {

            var getAllHisSkills = await skillsServices.GetAllInstructorSkills(instructorId);
            if (getAllHisSkills.IsError) return NotFound( new ApiResponce { ErrorMassages =  getAllHisSkills.FirstError.Description  });
            return Ok( new ApiResponce { Result = getAllHisSkills.Value });
        }

    }
}
