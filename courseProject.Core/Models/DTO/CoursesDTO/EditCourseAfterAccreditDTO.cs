using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.CoursesDTO
{
    public class EditCourseAfterAccreditDTO
    {
        public string? description { get; set; }
        [NotMapped] public IFormFile? image { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? Deadline { get; set; }
        public int? limitNumberOfStudnet { get; set; }
        public Guid? InstructorId { get; set; }
    }
}
