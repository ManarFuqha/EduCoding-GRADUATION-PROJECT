using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;


namespace courseProject.Services.Courses
{
    public class CourseServices : ICourseServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       

        public CourseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
        }

        public async Task<ErrorOr<Updated>> EditOnCourseAfterAccredit(Guid courseId, EditCourseAfterAccreditDTO editedCourse)
        {
          
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (getCourse == null) return ErrorCourse.NotFound;
         
            mapper.Map(editedCourse, getCourse);
            if (editedCourse.image != null)
            {
                getCourse.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(editedCourse.image);
            }
            getCourse.dateOfUpdated = DateTime.Now;
        
            
            if (getCourse.status.ToLower() != "accredit") return ErrorCourse.NoContent;          
            await unitOfWork.CourseRepository.updateCourse(getCourse);
            await unitOfWork.CourseRepository.saveAsync();
            return Result.Updated; 

        }

        public async Task<IReadOnlyList<Course>> GetAllCourses()
        {
            var courses = await unitOfWork.CourseRepository.GetAllCoursesAsync();
            foreach(var course in courses)
            {
                if(course.ImageUrl != null)
                {
                    course.ImageUrl=await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }

          
            return courses;
            
        }

       

        public async Task<ErrorOr<Course>> GetCourseById(Guid courseId)
        {           
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (getCourse.ImageUrl != null)
            {
                getCourse.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(getCourse.ImageUrl);
            }
           
       
            return getCourse is null ? ErrorCourse.NotFound : getCourse;
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesToStudent(Guid studentId)
        {
            var courses = await unitOfWork.CourseRepository.GetAllCoursesAsync(studentId);
            foreach (var course in courses)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }
     
            return courses;
            
        }

        public async Task<IReadOnlyList<Course>> GetAllCoursesForAccreditAsync()
        {
            var courses = await unitOfWork.CourseRepository.GetAllCoursesForAccreditAsync();          
            courses = courses.OrderByDescending(x => x.dateOfAdded).ToList();
            foreach (var course in courses)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }
        
            return courses;
        }

        public async Task<ErrorOr<Created>> createCourse(Course course)
        {
            var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(course.InstructorId, "instructor");
            if (instructorFound == null) return ErrorInstructor.NotFound;
            var SubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(course.subAdminId, "subadmin");
            var MainSubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(course.subAdminId, "main-subadmin");
            if (SubAdminFound == null && MainSubAdminFound==null) return ErrorSubAdmin.NotFound;
           
            if (course.image != null)
            {
                course.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(course.image);
            }
            using (var transaction = await unitOfWork.UserRepository.BeginTransactionAsync())
            {
                                  
                    await unitOfWork.CourseRepository.CreateCourse(course);
                    var success2 = await unitOfWork.StudentRepository.saveAsync();

                    if (success2 > 0)
                    {
                        await transaction.CommitAsync();
                        return Result.Created;
                    }

                    return ErrorCourse.hasError;
                
            }

               
        }

        public async Task<ErrorOr<Updated>> accreditCourse(Guid courseId, string Status)
        {
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);           
            if (getCourse == null) return ErrorCourse.NotFound;

            Expression<Func<Course, string>> path = x => x.status;
            var patchDocument = new JsonPatchDocument<Course>();
            patchDocument.Replace(path, Status);
            getCourse.status = Status;
            
            await unitOfWork.CourseRepository.updateCourse(getCourse);
            await unitOfWork.UserRepository.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EditOnCOurseBeforeAnAccredit(Guid courseId, CourseForEditDTO course)
        {
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (getCourse == null) return ErrorCourse.NotFound;
          
            mapper.Map(course, getCourse);
            if (course.image != null)
            {
                getCourse.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(course.image);
            }
            
            await unitOfWork.CourseRepository.updateCourse(getCourse);
            await unitOfWork.CourseRepository.saveAsync() ;
            return Result.Updated;

        }

        public async Task<ErrorOr<IReadOnlyList<Course>>> GetALlUndefinedCoursesForSubAdmins(Guid subAdminId)
        {
            var SubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(subAdminId, "subadmin");
            var MainSubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(subAdminId, "main-subadmin");
            if (SubAdminFound == null && MainSubAdminFound == null) return ErrorSubAdmin.NotFound;
            var allUndefinedCourses = await unitOfWork.CourseRepository.GetAllUndefinedCoursesBySubAdminIdAsync(subAdminId);
            foreach (var course in allUndefinedCourses)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }
       
            return allUndefinedCourses.ToErrorOr() ;

            
        }

        public async Task<ErrorOr<IReadOnlyList<Course>>> GetAllCoursesByInstructor(Guid instructorId)
        {
            var instructorFound = await unitOfWork.UserRepository.ViewProfileAsync(instructorId, "instructor");
            if (instructorFound == null) return ErrorInstructor.NotFound;
            var courseFond = await unitOfWork.CourseRepository.GetAllCoursesGivenByInstructorIdAsync(instructorId);
            foreach (var course in courseFond)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }
   
            return courseFond.ToErrorOr();
            
        }

        public async Task<IReadOnlyList<CustomCourseForRetriveDTO>> GetAllCustomCourses()
        {
            var GetCustomCourse = await unitOfWork.RequestRepository.GerAllCoursesRequestAsync();
            
            var CustomCoursesMapper = mapper.Map<IReadOnlyList<Request>, IReadOnlyList<CustomCourseForRetriveDTO>>(GetCustomCourse);
            return CustomCoursesMapper;
        }

        public async Task<ErrorOr<CustomCourseForRetriveDTO>> GetCustomCoursesById(Guid courseId)
        {
            var GetCustomCourse = await unitOfWork.RequestRepository.GerCourseRequestByIdAsync(courseId);
            if (GetCustomCourse == null) return ErrorCourse.NotFound;
            
            var CustomCoursesMapper = mapper.Map<Request, CustomCourseForRetriveDTO>(GetCustomCourse);
            return CustomCoursesMapper.ToErrorOr();
        }

        public async Task<ErrorOr<IReadOnlyList<StudentCourse>>> GetAllEnrolledCourses(Guid studentId)
        {
            var student = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (student == null) return ErrorStudent.NotFound;
            var enrolledCourses = await unitOfWork.CourseRepository.GetAllCoursesForStudentAsync(studentId);
            
            var courseFound = enrolledCourses.Select(x => x.Course).ToList();
            foreach (var course in courseFound)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(course.ImageUrl);
                }
            }
       
            return enrolledCourses.ToErrorOr();
        }
    }
}
