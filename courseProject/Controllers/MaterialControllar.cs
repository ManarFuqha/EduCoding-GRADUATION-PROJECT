using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using Microsoft.AspNetCore.Authorization;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Services.Materials;

namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialControllar : ControllerBase
    {
      
        private readonly IMaterialServices materialServices;
        

        public MaterialControllar( IMaterialServices materialServices )
        {
           
            this.materialServices = materialServices;
           
       }




        /// <summary>
        /// Adds a task.
        /// </summary>
        /// <param name="taskDTO">The DTO containing information about the task to add.</param>
        /// <returns>An IActionResult representing the result of adding the task or appropriate error responses.</returns>
        [HttpPost("AddTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddTask( [FromForm] TaskDTO taskDTO)
        {
            var addTask = await materialServices.AddTask(taskDTO);
            if (addTask.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result="Tha task is added successfully"});

        }




        /// <summary>
        /// Adds a file.
        /// </summary>
        [HttpPost("AddFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddFile([FromForm] FileDTO fileDTO)
        {
            var addfile = await materialServices.AddFile(fileDTO);
            if (addfile.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha file is added successfully" });

        }





        /// <summary>
        /// Adds an Announcement.
        /// </summary>
        [HttpPost("AddAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Instructor")]
        public async Task<IActionResult> AddAnnouncement( AnnouncementDTO AnnouncementDTO)
        {

            var addAnnouncement = await materialServices.AddAnnouncement(AnnouncementDTO);
            if (addAnnouncement.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha Announcement is added successfully" });

        }




        /// <summary>
        /// Adds a link.
        /// </summary>
        [HttpPost("AddLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Instructor")]
        public async Task<IActionResult> AddLink( LinkDTO linkDTO)
        {

            var addLink = await materialServices.AddLink(linkDTO);
            if (addLink.IsError) return Unauthorized();
            return Ok(new ApiResponce { Result = "Tha link is added successfully" });

        }





        /// <summary>
        /// Edits a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to edit.</param>
        /// <param name="taskDTO">The DTO containing updated information about the task.</param>
        /// <returns>An IActionResult representing the result of editing the task or appropriate error responses.</returns>
        [HttpPut("EditTask")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="InstructorwhoGiveTheMaterial")]
        
        public async Task<IActionResult> EditTask(Guid id, [FromForm] TaskForEditDTO taskDTO)
        {
            if(taskDTO.pdf!=null)await materialServices.deleteFiles(id);
            var editedTask = await materialServices.EditTask(id, taskDTO);
            if (editedTask.IsError) return NotFound(new ApiResponce { ErrorMassages=editedTask.FirstError.Description});
            return Ok(new ApiResponce { Result = "The task is updated successfully" });
        }





        /// <summary>
        /// Edits a file by ID.
        /// </summary>
        [HttpPut("EditFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditFile(Guid id, [FromForm] FileToEditDTO fileDTO)
        {
            if (fileDTO.pdf != null) await materialServices.deleteFiles(id);
            var editedFile = await materialServices.EditFile(id, fileDTO);
            if (editedFile.IsError) return NotFound(new ApiResponce { ErrorMassages = editedFile.FirstError.Description });
            return Ok(new ApiResponce { Result = "The file is updated successfully" });
        }





        /// <summary>
        /// Edits an Announcement by ID.
        /// </summary>
        [HttpPut("EditAnnouncement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditAnnouncement(Guid id, AnnouncementForEditDTO AnnouncementDTO)
        {
            var editedAnnouncement = await materialServices.EditAnnouncement(id, AnnouncementDTO);
            if (editedAnnouncement.IsError) return NotFound(new ApiResponce { ErrorMassages = editedAnnouncement.FirstError.Description });
            return Ok(new ApiResponce { Result = "The Announcement is updated successfully" });
        }





        /// <summary>
        /// Edits a link by ID.
        /// </summary>
        [HttpPut("EditLink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> EditLink(Guid id,  LinkForEditDTO linkDTO)
        {
            var editedLink = await materialServices.EDitLink(id, linkDTO);
            if (editedLink.IsError) return NotFound(new ApiResponce { ErrorMassages = editedLink.FirstError.Description });
            return Ok(new ApiResponce { Result = "The link is updated successfully" });
        }





        /// <summary>
        /// Deletes a material by ID.
        /// </summary>
        /// <param name="id">The ID of the material to delete.</param>
        /// <returns>An IActionResult representing the result of deleting the material or appropriate error responses.</returns>
        [HttpDelete("DeleteMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            var materail = await materialServices.DeleteMaterial(id);
            if (materail.IsError) return NotFound(new ApiResponce { ErrorMassages= materail.FirstError.Description} );
            return Ok(new ApiResponce { Result="The material is deleted successfully"});      
        }





        /// <summary>
        /// Retrieves material details by its ID.
        /// </summary>
        /// <param name="id">The ID of the material to retrieve.</param>
        /// <returns>An IActionResult representing the retrieved material or appropriate error responses.</returns>
        [HttpGet("GetMaterialById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "MaterialInEnrolledCourse")]
        public async Task<IActionResult> GetMaterialByIdAsync(Guid id)
        {
            var material = await materialServices.GetMaterialById(id);
            if (material.IsError) return NotFound(material.FirstError.Description);
            return Ok(new ApiResponce { Result = material.Value });
        }





        /// <summary>
        /// Retrieves all materials associated with a course or consultation.
        /// </summary>
        /// <param name="CourseId">Optional: The ID of the course to retrieve materials for.</param>
        /// <param name="ConsultationId">Optional: The ID of the consultation to retrieve materials for.</param>
        /// <returns>An IActionResult representing the retrieved materials or appropriate error responses.</returns>
        [HttpGet("GetAllMaterial")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "EnrolledInCourse")]
        public async Task<IActionResult> GetAllMaterialInTheCourseToStudentAsync( Guid? CourseId  , Guid? ConsultationId )
        {
            var AllMaterials = await materialServices.GetAllMaterialInTheCourse(CourseId , ConsultationId );
            if (AllMaterials.IsError) return NotFound(new ApiResponce { ErrorMassages=AllMaterials.FirstError.Description});
            return Ok(new ApiResponce { Result = AllMaterials.Value });
        }

         



        /// <summary>
        /// Changes the visibility status of a material.
        /// </summary>
        /// <param name="Id">The ID of the material to change its status.</param>
        /// <param name="isHidden">Boolean flag indicating whether the material should be hidden (true) or shown (false).</param>
        /// <returns>An IActionResult representing the status change or appropriate error responses.</returns>
        [HttpPatch("HideOrShowMaterials")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "InstructorwhoGiveTheMaterial")]
        public async Task<IActionResult> HideOrShowMaterials(Guid Id, bool isHidden)
        {
            var statusMaterial = await materialServices.changeMaterialStatus(Id, isHidden);
            if (statusMaterial.IsError) return NotFound(new ApiResponce { ErrorMassages=statusMaterial.FirstError.Description});
            return Ok(new ApiResponce { Result="The status of material is changed successfully"});
        }


        

    }
}
