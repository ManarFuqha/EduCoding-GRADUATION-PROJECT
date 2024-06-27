using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using ErrorOr;

using Microsoft.AspNetCore.Mvc;

namespace courseProject.Services.Users
{
    public interface IUserServices
    {

        public Task<ErrorOr<User>> getUserById(Guid userId);
        public Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO);
        public Task<ErrorOr<Created>> Register(RegistrationRequestDTO model);
        public Task<ErrorOr<Success>> addCodeVerification(string email, string code);
        public Task<ErrorOr<Success>> reSendTheVerificationCode(string email);
        public Task<ErrorOr<Updated>> EditUserProfile(Guid id,  ProfileDTO profile);
        public Task<ErrorOr<UserInfoDTO>> GetProfileInfo(Guid id);
        public Task<ErrorOr<Updated>> changePassword(Guid UserId, ChengePasswordDTO changePasswordDTO);
        public Task<ErrorOr<Success>> GetUserByEmail (string email);
        public Task<ErrorOr<Success>> forgetPassword(string email, ForgetPasswordDTO forgetPassword);

    }
}
