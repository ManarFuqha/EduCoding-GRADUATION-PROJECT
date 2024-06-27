using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.Models.DTO.UsersDTO;

namespace courseProject.Core.Models.DTO.LecturesDTO
{
    public class PublicLectureForRetriveDTO
    {
        public Guid consultationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public Guid InstructorId { get; set; }
        public string Duration { get; set; }
        public Guid StudentId { get; set; }
        public string date { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string InstructoruserName { get; set; }
        public string? InstructorLName { get; set; }
        //public List<Student> students { get; set; } = new List<Student>();

        public List<UserNameDTO> Students { get; set; }
        //  public List<string?> StudentLName { get; set; }
    }
}
