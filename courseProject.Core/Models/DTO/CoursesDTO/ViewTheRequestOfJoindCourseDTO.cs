using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models.DTO.CoursesDTO
{
    public class ViewTheRequestOfJoindCourseDTO
    {
        public string StudentName { get; set; }
        public Guid StudentId { get; set; }
        public string CourseName { get; set; }
        public Guid courseId { get; set; }
        public string status { get; set; }
        public string EnrollDate { get; set; }


    }
}
