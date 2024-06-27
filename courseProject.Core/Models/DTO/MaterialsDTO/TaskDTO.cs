using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.MaterialsDTO
{
    public class TaskDTO
    {

        public string name { get; set; }

        public string? description { get; set; }

        [NotMapped] public List<IFormFile> pdf { get; set; }

        // [NotMapped] public List<IFormFile> Listpdf { get; set; }
        public DateTime? DeadLine { get; set; }
        public Guid? courseId { get; set; }
        public Guid? consultationId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
