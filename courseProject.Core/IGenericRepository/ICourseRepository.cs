using courseProject.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ICourseRepository<T> :IGenericRepository1<Course>
    {
        public  Task<T> GetCourseByIdAsync(Guid? id);
        public Task<int> GetNumberOfStudentsInTHeCourseAsync(Guid courseId);
    
       
        public Task<IReadOnlyList<Course>> GetAllUndefinedCoursesBySubAdminIdAsync(Guid subAdminId);
        public Task<Course> getAccreditCourseByIdAcync(Guid courseId);
        public Task<IReadOnlyList<Course>> GetAllCoursesGivenByInstructorIdAsync(Guid Instructorid);
        public Task<IReadOnlyList<StudentCourse>> GetAllCoursesForStudentAsync(Guid Studentid);
        public Task<IReadOnlyList<Course>> GetAllCoursesAsync(Guid studentId);
        public Task CreateCourse(Course model);
        public Task updateCourse(Course course);
       public Task<IReadOnlyList<Course>> GetAllCoursesAsync();
        public Task<IReadOnlyList<Course>> GetAllCoursesForAccreditAsync();
    }
}
