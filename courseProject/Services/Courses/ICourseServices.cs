using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;


namespace courseProject.Services.Courses
{
    public interface ICourseServices
    {
        public Task<ErrorOr<Updated>> EditOnCourseAfterAccredit(Guid courseId, EditCourseAfterAccreditDTO editedCourse);
        public Task<ErrorOr<Course>> GetCourseById(Guid courseId);
        public Task<IReadOnlyList<Course>> GetAllCourses();
        public Task<IReadOnlyList<Course>> GetAllCoursesToStudent(Guid studentId);
        public Task<IReadOnlyList<Course>> GetAllCoursesForAccreditAsync();
        public Task<ErrorOr<Created>> createCourse(Course course  );
        public Task<ErrorOr<Updated>> accreditCourse(Guid courseId, string Status);
        public Task<ErrorOr<Updated>> EditOnCOurseBeforeAnAccredit(Guid courseId , CourseForEditDTO course);
        public Task<ErrorOr<IReadOnlyList<Course>>> GetALlUndefinedCoursesForSubAdmins(Guid subAdminId);
        public Task<ErrorOr<IReadOnlyList<Course>>> GetAllCoursesByInstructor(Guid instructorId);
        public Task<IReadOnlyList<CustomCourseForRetriveDTO>> GetAllCustomCourses();
        public Task<ErrorOr<CustomCourseForRetriveDTO>> GetCustomCoursesById(Guid courseId);
       public Task<ErrorOr<IReadOnlyList<StudentCourse>>> GetAllEnrolledCourses(Guid studentId);
    }
}
