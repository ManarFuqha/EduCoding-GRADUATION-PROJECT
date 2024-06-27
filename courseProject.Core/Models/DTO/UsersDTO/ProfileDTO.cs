using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.UsersDTO
{
    public class ProfileDTO
    {

        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? gender { get; set; }
        public string? phoneNumber { get; set; }

        
        [NotMapped] public IFormFile? image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //  public string? email { get; set; }
        public string? address { get; set; }


    }
}
