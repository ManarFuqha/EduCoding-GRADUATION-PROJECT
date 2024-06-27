using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.MaterialsDTO
{
    public class AnnouncementDTO
    {
        public string name { get; set; }

        public string? description { get; set; }
        public Guid? courseId { get; set; }
        public Guid? consultationId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
