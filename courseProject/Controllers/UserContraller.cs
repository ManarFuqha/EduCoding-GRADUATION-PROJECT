using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using courseProject.Core.Models;
using System.Security.Claims;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using Microsoft.Extensions.Caching.Memory;
using courseProject.Services.Users;



namespace courseProject.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserContraller : ControllerBase
    {
       
      
        protected ApiResponce response;
   
        private readonly IMemoryCache memoryCache;
        private readonly IUserServices userServices;

        public UserContraller( IMemoryCache memoryCache , IUserServices userServices)
        {

            this.memoryCache = memoryCache;
            this.userServices = userServices;
        }





        /// <summary>
        /// Endpoint to retrieve the User ID from the token.
        /// </summary>
        /// <returns>An IActionResult containing the User ID extracted from the token.</returns>
        [HttpGet("GetUserIdFromToken")]
        [Authorize]
        public IActionResult GetUserIdFromToken()
        {
            var Id = HttpContext.User.FindFirstValue("UserId");          
            if (Id==null)
            {
                return Unauthorized("User ID not found in token");
            }
            return Ok(Id);
        }







        /// <summary>
        /// Endpoint for user login.
        /// </summary>
        /// <param name="loginRequestDTO">DTO containing login credentials.</param>
        /// <returns>An IActionResult containing the result of the login attempt.</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        
        public async Task<IActionResult> Login( LoginRequestDTO loginRequestDTO)
        {
            var login = await userServices.Login(loginRequestDTO);
            if (login.IsError) return Ok(new ApiResponce { ErrorMassages = login.FirstError.Description });
            return Ok(new ApiResponce { Result = login.Value });
        }








        /// <summary>
        /// Endpoint for user registration.
        /// </summary>
        /// <param name="model">DTO containing registration details.</param>
        /// <returns>An IActionResult containing the result of the registration attempt.</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            var registeUser = await userServices.Register(model);
            if (registeUser.IsError) return Ok(new ApiResponce { ErrorMassages=registeUser.FirstError.Description});
            return Ok(new ApiResponce { Result = model});
        }








        /// <summary>
        /// Endpoint to add verification code for email.
        /// </summary>
        /// <param name="email">The email address to add the verification code.</param>
        /// <param name="code">The verification code to be added.</param>
        /// <returns>An IActionResult indicating success or failure of the code addition.</returns>
        [HttpPost("addCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCode(string email, string code)
        {
            var codeAdded = await userServices.addCodeVerification(email, code);
            if(codeAdded.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages=codeAdded.FirstError.Description });
            else if (codeAdded.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = codeAdded.FirstError.Description });
            return Ok(new ApiResponce { Result="you are verified now"});
        }








        /// <summary>
        /// Endpoint to resend verification code to email.
        /// </summary>
        /// <param name="email">The email address to resend the verification code.</param>
        /// <returns>An IActionResult indicating success or failure of resending the code.</returns>
        [HttpGet("reSendCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> reSendTheVerificationCode(string email)
        {
            var recendCode = await userServices.reSendTheVerificationCode(email);
            if (recendCode.FirstError.Type == ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = recendCode.FirstError.Description });
            else if (recendCode.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = recendCode.FirstError.Description });
            return Ok(new ApiResponce { Result = "Another code has been sent" });
            
        }








        /// <summary>
        /// Endpoint for editing user profile.
        /// </summary>
        /// <param name="id">The ID of the user whose profile is being edited.</param>
        /// <param name="profile">DTO containing updated profile information.</param>
        /// <returns>An IActionResult indicating success or failure of the profile update.</returns>
        [HttpPut("EditProfile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> EditProfileAsync(Guid id,[FromForm] ProfileDTO profile)
        {
            var updatedProfile = await userServices.EditUserProfile(id, profile);
            if (updatedProfile.FirstError.Type == ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages = updatedProfile.FirstError.Description });
            else if (updatedProfile.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = updatedProfile.FirstError.Description });
            return Ok(new ApiResponce { Result = "Profile Updated Successfully." });
        }







        /// <summary>
        /// Endpoint to retrieve user profile information.
        /// </summary>
        /// <param name="id">The ID of the user whose profile information is requested.</param>
        /// <returns>An IActionResult containing the user's profile information.</returns>
        [HttpGet("GetProfileInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var getUser = await userServices.GetProfileInfo(id);
            if (getUser.IsError) return NotFound(new ApiResponce {ErrorMassages=getUser.FirstError.Description });
            return Ok(new ApiResponce { Result=getUser.Value});
        }








        /// <summary>
        /// Endpoint for changing user password.
        /// </summary>
        /// <param name="UserId">The ID of the user whose password is being changed.</param>
        /// <param name="chengePasswordDTO">DTO containing the new password.</param>
        /// <returns>An IActionResult indicating success or failure of the password change.</returns>
        [HttpPatch("changePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> changePassword (Guid UserId ,ChengePasswordDTO chengePasswordDTO)
        {
            var changedPass = await userServices.changePassword( UserId, chengePasswordDTO);
            if (changedPass.FirstError.Type==ErrorOr.ErrorType.NotFound) return NotFound(new ApiResponce { ErrorMassages=changedPass.FirstError.Description});
            else if (changedPass.FirstError.Type == ErrorOr.ErrorType.Validation) return Ok(new ApiResponce { ErrorMassages = changedPass.FirstError.Description });
            return Ok(new ApiResponce { Result="Password Changed Successfully."});
        }







        /// <summary>
        /// Endpoint to add email for forget password.
        /// </summary>
        /// <param name="emailDTO">DTO containing the email address.</param>
        /// <returns>An IActionResult indicating success or failure of adding the email.</returns>
        [HttpPost("AddEmailForForgetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> addEmail (EmailDTO emailDTO)
        {
            var user = await userServices.GetUserByEmail(emailDTO.email);
            if (user.IsError) return NotFound(new ApiResponce { ErrorMassages=user.FirstError.Description});
            return Ok(new ApiResponce { Result=emailDTO.email});
        }







        /// <summary>
        /// Endpoint for forgetting user password.
        /// </summary>
        /// <param name="email">The email address of the user whose password is being reset.</param>
        /// <param name="forgetPassword">DTO containing the new password.</param>
        /// <returns>An IActionResult indicating success or failure of the password reset.</returns>
        [HttpPatch("ForgetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ForgetPassword(string email ,ForgetPasswordDTO forgetPassword)
        {
            var addPassword = await userServices.forgetPassword(email, forgetPassword);
            if (addPassword.IsError) return NotFound(new ApiResponce { ErrorMassages=addPassword.FirstError.Description});
            return Ok(new ApiResponce {Result= "The password has been modified successfully" });
        }
    }
}
