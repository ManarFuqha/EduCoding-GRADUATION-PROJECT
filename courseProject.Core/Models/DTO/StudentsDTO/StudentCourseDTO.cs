using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.StudentsDTO
{
    public class StudentCourseDTO
    {
        public Guid StudentId { get; set; }
        public Guid courseId { get; set; }
    }
}
