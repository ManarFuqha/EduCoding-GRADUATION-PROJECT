using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IUnitOfWork
    {

      
        public IUserRepository UserRepository { get; set; }
        public IinstructorRepositpry instructorRepositpry { get; set; }
        public IStudentRepository StudentRepository { get; set; }
        
        public IFileRepository FileRepository { get; set; }
        public ICourseRepository<Course> CourseRepository { get; set; }
        public IMaterialRepository materialRepository { get; set; }
        public IEventRepository eventRepository {  get; set; }
        public IEmailService EmailService { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set; }
        public ILecturesRepository lecturesRepository { get; set; }
        public IRequestRepository RequestRepository { get; set; }
        public ISkillRepository skillRepository { get; set; }
        public IStudentCourseRepository studentCourseRepository { get; set; }
        public ISubmissionRepository submissionRepository { get; set; }
        public IContactRepository contactRepository { get; set; }


    }
}
