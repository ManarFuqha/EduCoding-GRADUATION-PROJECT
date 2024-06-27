using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.UsersDTO
{
    public class UserInfoDTO
    {
        public Guid UserId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string? LName { get; set; }
        public string? DateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public string? gender { get; set; }
        public string? address { get; set; }
        public string? ImageUrl { get; set; }
        public string? skillDescription { get; set; }
    }
}
