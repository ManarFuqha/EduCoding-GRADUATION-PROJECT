using courseProject.Core.Models.DTO.StudentsDTO;
using ErrorOr;

namespace courseProject.Services.StudentCourses
{
    public interface IStudentCoursesServices
    {
        public Task<ErrorOr<Created>> EnrollInCourse(StudentCourseDTO studentCourseDTO);
        public Task<ErrorOr<Updated>> ApprovelToJoinCourse(Guid courseId, Guid studentId, string status);
    }
}
