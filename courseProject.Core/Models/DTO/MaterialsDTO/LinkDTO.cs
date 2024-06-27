using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.MaterialsDTO
{
    public class LinkDTO
    {
        public string name { get; set; }
        public string linkUrl { get; set; }
        public Guid? courseId { get; set; }
        public Guid? consultationId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
