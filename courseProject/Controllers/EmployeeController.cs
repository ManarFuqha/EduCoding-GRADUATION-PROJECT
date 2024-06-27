using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using courseProject.Repository.GenericRepository;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Services.Employees;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
  
        private readonly IEmployeeServices employeeServices;

        public EmployeeController( IEmployeeServices employeeServices )                       
        {        
            this.employeeServices = employeeServices;
        }


        /// <summary>
        /// Retrieves all employees with pagination.
        /// </summary>
        /// <param name="paginationRequest">The pagination request containing page number and page size.</param>
        [HttpGet("GetAllEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]      
        public async Task<IActionResult> GetAllEmployeeAsync([FromQuery] PaginationRequest paginationRequest)
        {
            var allEmployees = await employeeServices.getAllEmployees();         
            return Ok(new ApiResponce { Result = (Pagination<EmployeeDto>.CreateAsync(allEmployees, paginationRequest.pageNumber, paginationRequest.pageSize)).Result });               
        }



        /// <summary>
        /// Creates a new employee. This action can only be performed by an admin.
        /// </summary>
        /// <param name="model">The DTO containing the details of the employee to create.</param>
        /// <returns>An IActionResult indicating the outcome of the employee creation.</returns>

        [HttpPost("CreateEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> CreateEmployee([FromForm]EmployeeForCreate model)
        {
          
            var createEmployee = await employeeServices.CreateEmployee(model);
            if (createEmployee.FirstError.Type == ErrorOr.ErrorType.Failure) return BadRequest(new ApiResponce { 
                ErrorMassages= createEmployee.FirstError.Description } );

            else if (createEmployee.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok( new ApiResponce { 
                ErrorMassages =  createEmployee.FirstError.Description  });

             return Ok(new ApiResponce { Result="The employee is create successfully" } );
        }



        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>An IActionResult containing the employee details or appropriate error responses.</returns>
        [HttpGet("GetEmployeeById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {

            var getEmployee = await employeeServices.GetEmployeeById(id);
            if(getEmployee.IsError) return NotFound(new ApiResponce { ErrorMassages= getEmployee.FirstError.Description });
            return Ok(new ApiResponce { Result = getEmployee.Value });
        }



        /// <summary>
        /// Updates employee details from an admin perspective.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="EmpolyeeModel">The DTO containing the updated employee details.</param>
        [HttpPut("UpdateEmployeeFromAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> updateEmployee(Guid id,[FromForm] EmployeeForUpdateDTO EmpolyeeModel)
        {
            var updateEmployee = await employeeServices.UpdateEmployeeFromAdmin(id, EmpolyeeModel);
            if(updateEmployee.FirstError.Type==ErrorOr.ErrorType.NotFound) 
                return NotFound(new ApiResponce { ErrorMassages = updateEmployee.FirstError.Description });
            else if(updateEmployee.FirstError.Type == ErrorOr.ErrorType.Failure) return BadRequest(new ApiResponce { ErrorMassages = updateEmployee.FirstError.Description });

            return Ok(new ApiResponce { Result ="The employee is updated successfully"});
        }




        /// <summary>
        /// Edits the role of a user between SubAdmin and MainSubAdmin roles.
        /// </summary>
        /// <param name="userId">The ID of the user whose role is to be edited.</param>
        /// <param name="role">The new role to assign to the user.</param>
        [HttpPatch("EditroleBetweenSubAdmin&MainSubAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> EditRole (Guid userId ,string role)
        {
            var editRole = await employeeServices.EditRole(userId, role);
            if (editRole.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce {ErrorMassages=editRole.FirstError.Description });
            else if (editRole.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = editRole.FirstError.Description });

            return Ok(new ApiResponce { Result = "The role is edited successfully" });
        }

    }
}
