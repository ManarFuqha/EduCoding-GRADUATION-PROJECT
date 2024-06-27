using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.MaterialsDTO
{
    public class CourseMaterialDTO
    {

        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string pdfUrl { get; set; }
        public Guid InstructorId { get; set; }
        public Guid courseId { get; set; }
    }
}
