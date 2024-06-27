using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using courseProject.Emails;


namespace courseProject.Services.StudentCourses
{
    public class StudentCoursesServices : IStudentCoursesServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public StudentCoursesServices(IUnitOfWork unitOfWork , IMapper mapper , IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
        }

       

        public async Task<ErrorOr<Created>> EnrollInCourse(StudentCourseDTO studentCourseDTO)
        {
            
                var foundStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentCourseDTO.StudentId);
            if (foundStudent == null) return ErrorStudent.NotFound;
            var accreditCourse = await unitOfWork.CourseRepository.getAccreditCourseByIdAcync(studentCourseDTO.courseId);
            
            if(accreditCourse == null) return ErrorCourse.NotFound;
                var studnetNumber = await unitOfWork.CourseRepository.GetNumberOfStudentsInTHeCourseAsync(studentCourseDTO.courseId);
            if (studnetNumber >= accreditCourse.limitNumberOfStudnet)
                return ErrorCourse.fullCourse;
                var mapped = mapper.Map<StudentCourseDTO, StudentCourse>(studentCourseDTO);
                await unitOfWork.StudentRepository.EnrollCourse(mapped);
                 await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
            }


        public async Task<ErrorOr<Updated>> ApprovelToJoinCourse(Guid courseId, Guid studentId, string status)
        {
            var getStudentCourse = await unitOfWork.StudentRepository.GetFromStudentCourse(courseId, studentId);
            if (getStudentCourse == null) return ErrorStudentCourse.NotFound;
            
            Expression<Func<StudentCourse, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<StudentCourse>();
            patchDocument.Replace(path, status);
            getStudentCourse.status = status;

            await unitOfWork.studentCourseRepository.UpdateStudentCourse(getStudentCourse);
            var studentEmail = (await unitOfWork.UserRepository.getUserByIdAsync(studentId)).email;
            string studentName = (await unitOfWork.UserRepository.ViewProfileAsync(studentId, "student")).userName;
            var courseName = (await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId)).name;
            if (status.ToLower() == "reject")
            {
                await unitOfWork.StudentRepository.RemoveTheRejectedRequestToJoinCourse(getStudentCourse);
                
                await emailService.SendEmail (studentEmail , "Course Application Status", EmailTexts.RejectInCourse(studentName , courseName , "Course Academy"));
            }
            await emailService.SendEmail(studentEmail, "Course Application Status", EmailTexts.GetAcceptanceEmailHtml(studentName, courseName, "Course Academy"));

            await unitOfWork.CourseRepository.saveAsync();

            return Result.Updated;
        }

        
    }
    }

