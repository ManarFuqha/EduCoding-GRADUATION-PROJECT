using System.ComponentModel.DataAnnotations;

namespace courseProject.Core.Models.DTO.RegisterDTO
{
    public class RegistrationRequestDTO
    {
        //public int UserId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password {get; set; }
        public string ConfirmPassword { get; set; }
        public string role { get; set; }
    }
}
