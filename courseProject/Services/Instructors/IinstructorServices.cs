using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Services.Instructors
{
    public interface IinstructorServices
    {
        public Task<IReadOnlyList<User>> GetAllInstructors();
      //  public Task<ErrorOr<Instructor>> getInstructorById(Guid InstructorId);
        public Task<ErrorOr<Created>> AddOfficeHours(Guid InstructorId, WorkingHourDTO _Working_Hours);
        public Task<ErrorOr<IReadOnlyList<GetWorkingHourDTO>>> GetInstructorOfficeHours(Guid InstructorId);
        public Task<IReadOnlyList<EmployeeListDTO>> GetAllInstructorsList();
        public Task<IReadOnlyList<Instructor_OfficeHoursDTO>> GetAllInstructorsOfficeHours();
        public Task<ErrorOr<IReadOnlyList<EmployeeListDTO>>> GetListOfInstructorForLectures(LectureFormDTO lectureForm);
        public Task<ErrorOr<Updated>> AddSkillDescription(Guid instructorId , SkillDescriptionDTO skillDescription);
    }
}
