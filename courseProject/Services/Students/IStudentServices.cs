using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using ErrorOr;

namespace courseProject.Services.Students
{
    public interface IStudentServices
    {

        public Task<IReadOnlyList<StudentsInformationDto>> GetAllStudents();
      //  public Task<IReadOnlyList<ContactDto>> GetAllStudentsForContact();
        public Task<ErrorOr<IReadOnlyList<StudentsInformationDto>>> GetCourseParticipants(Guid courseId);

    }
}
