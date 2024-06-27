using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.CoursesDTO
{
    public class CustomCourseForRetriveDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public Guid StudentId { get; set; }
        public string StudentFName { get; set; }
        public string? StudentLName { get; set; }
    }
}
