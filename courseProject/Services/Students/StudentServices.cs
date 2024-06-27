using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.ServiceErrors;
using ErrorOr;


namespace courseProject.Services.Students
{
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
    

        public StudentServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
          
        }

        public async Task<IReadOnlyList<StudentsInformationDto>> GetAllStudents()
        {
            var Students = await unitOfWork.StudentRepository.GetAllStudentsAsync();
            foreach(var student in Students)
            {
                if (student.ImageUrl != null)
                {
                    student.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(student.ImageUrl);
                }
            }
         
            var mappedStudentDTO = mapper.Map<IReadOnlyList<User>, IReadOnlyList<StudentsInformationDto>>(Students);
            return mappedStudentDTO;
        }

       

        public async Task<ErrorOr<IReadOnlyList<StudentsInformationDto>>> GetCourseParticipants(Guid courseId)
        {
            var courseFound = await unitOfWork.CourseRepository.getAccreditCourseByIdAcync(courseId);
            if (courseFound == null) return ErrorCourse.NotFound;
            var GetStudents = await unitOfWork.StudentRepository.GetAllStudentsInTheSameCourseAsync(courseId);
            var StudentMapper = mapper.Map<IReadOnlyList<User>, IReadOnlyList<StudentsInformationDto>>(GetStudents);
            return StudentMapper.ToErrorOr();
                
        }
    }
}
