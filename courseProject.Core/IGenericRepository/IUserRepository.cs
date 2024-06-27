using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;


namespace courseProject.Core.IGenericRepository
{
    public interface IUserRepository :IGenericRepository1<User>
    {

     Task< LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO); 
     Task<User>  RegisterAsync(RegistrationRequestDTO registerRequestDTO);

       bool isUniqeUser(string email);

        public Task<User> GetUserByRoleAsync(string role);
        public Task<User> getUserByIdAsync(Guid UserId);
        public Task<IReadOnlyList< User>> getAllMainSubAmdinRole();
        public Task<string> GenerateSecureVerificationCode(int length);
        public Task<User> GetUserByEmail(string email);
        public Task UpdateUser(User user);

        public Task<User> getRoleFromToken();
        public  Task<User> createEmployeeAccount(User user);

        public Task<User> ViewProfileAsync(Guid id, string role);
        public Task editRole(User user);
        // void LogOut();
    }
}
