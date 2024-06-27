using Microsoft.Extensions.Configuration;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;

using courseProject.Core.Models;
using Microsoft.AspNetCore.Http;


namespace courseProject.Repository.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {

       
    


        public UnitOfWork(projectDbContext dbContext , IConfiguration configuration , IHttpContextAccessor httpContextAccessor )
        {
        
           
          //  SubAdminRepository = new SubAdminRepository(dbContext);
            UserRepository = new UserRepository(dbContext, configuration, httpContextAccessor);
            instructorRepositpry = new instructorRepositpry(dbContext);
            StudentRepository=new StudentRepository(dbContext);          
            FileRepository = new FileRepository(httpContextAccessor);
            CourseRepository = new CourseRepository(dbContext);
            materialRepository = new MaterialRepository(dbContext);
            eventRepository = new EventRepository(dbContext);
            FeedbackRepository = new FeedbackRepository(dbContext);
            lecturesRepository = new LecturesRepository(dbContext);
            RequestRepository = new RequestRepository(dbContext);
            skillRepository= new SkillRepository(dbContext);
            studentCourseRepository = new StudentCourseRepository(dbContext);
            submissionRepository = new SubmissionRepository(dbContext);
            contactRepository = new ContactRepository(dbContext);
 

        }

        public ISubAdminRepository SubAdminRepository { get; set; }
        public IUserRepository UserRepository { get; set ; }
        public IinstructorRepositpry instructorRepositpry { get ; set ; }
        public IStudentRepository StudentRepository { get; set ; }       
        public IFileRepository FileRepository { get; set; }
        public ICourseRepository<Course> CourseRepository { get; set ; }
        public IMaterialRepository materialRepository { get; set ; }
        public IEventRepository eventRepository { get; set ; }
        public IEmailService EmailService { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set ; }
        public ILecturesRepository lecturesRepository { get; set ; }
        public IRequestRepository RequestRepository { get; set ; }
        public ISkillRepository skillRepository { get; set ; }
        public IStudentCourseRepository studentCourseRepository { get; set ; }
        public ISubmissionRepository submissionRepository { get; set ; }
        public IContactRepository contactRepository { get; set ; }
    
    }
}

