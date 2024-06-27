using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.InstructorsDTO;
using courseProject.Core.Models.DTO.LecturesDTO;
using courseProject.ServiceErrors;
using ErrorOr;


namespace courseProject.Services.Instructors
{
    public class instructorServices : IinstructorServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        

        public instructorServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
         
        }

       

        public async Task<IReadOnlyList<User>> GetAllInstructors()
        {
            return await unitOfWork.instructorRepositpry.getAllInstructors();
        }

        public async Task<ErrorOr<User>> getInstructorById(Guid InstructorId)
        {
          //  var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(InstructorId, "instructor");
            
            var getInstructor =  await unitOfWork.UserRepository.getUserByIdAsync(InstructorId);
            if (getInstructor == null || getInstructor.role.ToLower()!="instructor") return ErrorInstructor.NotFound;
            return getInstructor;
        }

        public async Task<ErrorOr<Created>> AddOfficeHours(Guid InstructorId, WorkingHourDTO _Working_Hours)
        {
           
            var instructorFound = await unitOfWork.UserRepository.getUserByIdAsync(InstructorId);
            if (instructorFound == null || instructorFound.role.ToLower()!="instructor") return ErrorInstructor.NotFound;

            if (!CommonClass.IsValidTimeFormat(_Working_Hours.startTime) || !CommonClass.IsValidTimeFormat(_Working_Hours.endTime))
                return ErrorInstructor.InvalidTime;
            var OfficeHourMapper = mapper.Map<WorkingHourDTO, Instructor_Working_Hours>(_Working_Hours);
            if (!CommonClass.CheckStartAndEndTime(OfficeHourMapper.startTime, OfficeHourMapper.endTime))
                return ErrorInstructor.InvalidTime;
            OfficeHourMapper.InstructorId = InstructorId;
            await unitOfWork.instructorRepositpry.AddOfficeHoursAsync(OfficeHourMapper);
            await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<IReadOnlyList<GetWorkingHourDTO>>> GetInstructorOfficeHours(Guid InstructorId)
        {
            var instructorFound = await unitOfWork.UserRepository.getUserByIdAsync(InstructorId);
            if (instructorFound == null || instructorFound.role.ToLower()!="instructor")
                return ErrorInstructor.NotFound;
            var InstructorOfficeHours = await unitOfWork.instructorRepositpry.GetOfficeHourByIdAsync(InstructorId);
            var InstructorOfficeHoursMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<GetWorkingHourDTO>>(InstructorOfficeHours);
            return InstructorOfficeHoursMapper.ToErrorOr();
        }

        public async Task<IReadOnlyList<EmployeeListDTO>> GetAllInstructorsList()
        {
            var GetInstructors = await unitOfWork.instructorRepositpry.GetAllEmployeeAsync();
           
            var CustomCoursesMapper = mapper.Map<IReadOnlyList<User>, IReadOnlyList<EmployeeListDTO>>(GetInstructors);
            return CustomCoursesMapper;
        }

        public async Task<IReadOnlyList<Instructor_OfficeHoursDTO>> GetAllInstructorsOfficeHours()
        {
            var AllOfficeHours = await unitOfWork.instructorRepositpry.getAllInstructorsOfficeHoursAsync();
           
            var InstrctorOfficeHoursMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<Instructor_OfficeHoursDTO>>(AllOfficeHours);
            return InstrctorOfficeHoursMapper;
        }

        public async Task<ErrorOr< IReadOnlyList<EmployeeListDTO>>> GetListOfInstructorForLectures(LectureFormDTO lectureForm)
        {
           
            TimeSpan StartTime = CommonClass.ConvertToTimeSpan(lectureForm.startTime);
            TimeSpan EndTime = CommonClass.ConvertToTimeSpan(lectureForm.endTime);
            if (StartTime >= EndTime) return ErrorLectures.GraterTime;

            if ((EndTime - StartTime) > TimeSpan.Parse("02:00") || (EndTime - StartTime) < TimeSpan.Parse("00:30"))
                return ErrorLectures.limitationTime;
            var getAllSkills = await unitOfWork.skillRepository.GetAllSkillsAsync();
            
            var getInstructors = await unitOfWork.instructorRepositpry.getAListOfInstructorDependOnSkillsAndOfficeTime(lectureForm.skillId, StartTime, EndTime, lectureForm.date);           
            var instructorMapper = mapper.Map<IReadOnlyList<Instructor_Working_Hours>, IReadOnlyList<EmployeeListDTO>>(getInstructors);            
            return instructorMapper.ToErrorOr();
            
        }

        public async Task<ErrorOr<Updated>> AddSkillDescription(Guid instructorId, SkillDescriptionDTO skillDescriptionDTO)
        {
            var instructor = await unitOfWork.UserRepository.ViewProfileAsync(instructorId , "instructor");
            if (instructor == null) return ErrorInstructor.NotFound;

            instructor.skillDescription = skillDescriptionDTO.skillDescription;
            await unitOfWork.UserRepository.UpdateUser(instructor);
            await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }
    }
}
