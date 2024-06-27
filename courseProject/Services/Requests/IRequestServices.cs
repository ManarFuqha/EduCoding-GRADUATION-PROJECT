using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.Core.Models.DTO.StudentsDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Services.Requests
{
    public interface IRequestServices
    {

        public Task<IReadOnlyList<ViewTheRequestOfJoindCourseDTO>> GetAllRequestToJoinCourses();
        public Task<ErrorOr<Created>> RequestToCreateCustomCourse(Guid studentid, StudentCustomCourseDTO studentCustomCourse);

    }
}
