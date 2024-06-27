using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IStudentCourseRepository
    {

        public Task UpdateStudentCourse(StudentCourse studentCourse);



    }
}
