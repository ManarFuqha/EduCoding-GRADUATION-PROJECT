using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using Microsoft.AspNetCore.Authorization;
using courseProject.Services.ContactUs;
using courseProject.Core.Models.DTO.ContactUsDTO;
using courseProject.Repository.GenericRepository;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactServices contactServices;
        

        public ContactController(  IContactServices contactServices )
        {
            
            this.contactServices = contactServices;
          
        }



        /// <summary>
        /// Adds a new message to the contact us section.
        /// </summary>
        /// <param name="contact">The message details submitted by the user.</param>
        /// <returns>
        /// An IActionResult indicating the status of the message submission.
        /// </returns>
        /// <response code="200">Indicates that the message was successfully sent.</response>
        /// <response code="404">If the requested resource is not found.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost("MessageToContactUs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> AddAMessageToContactUs([FromForm] CreateMessageContactDTO contact)
        {
            await contactServices.AddNewCOntactMessage(contact);
            return Ok("The message is send successfully");
        }




        /// <summary>
        /// Retrieves all contact messages with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The page size for pagination.</param>
        /// <returns>
        /// An IActionResult containing a paginated list of contact messages.
        /// </returns>
        /// <response code="200">Returns the paginated list of contact messages.</response>
        /// <response code="404">If the requested resource is not found.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpGet("GetAllContact")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Main_Sub-Admin")]
        
        public async Task<IActionResult> GetAllContact([FromQuery] PaginationRequest paginationRequest )
        {
            var allContact = await contactServices.GetAllMessages();
            return Ok(Pagination<Contact>.CreateAsync(allContact , paginationRequest.pageNumber, paginationRequest.pageSize).Result);
        }



        /// <summary>
        /// Retrieves a contact message by its ID.
        /// </summary>
        /// <param name="Contactid">The ID of the contact message to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the contact message with the specified ID.
        /// </returns>
        /// <response code="200">Returns the contact message with the specified ID.</response>
        /// <response code="404">If the requested contact message is not found.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost("GetContactById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin , Main_Sub-Admin")]

        public async Task<IActionResult> GetContactById(Guid Contactid)
        {
            return Ok(await contactServices.getContactById(Contactid));
        }



      

    }
}
