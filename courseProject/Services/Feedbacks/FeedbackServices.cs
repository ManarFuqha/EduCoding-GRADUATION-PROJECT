using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.ServiceErrors;
using ErrorOr;


namespace courseProject.Services.Feedbacks
{
    public class FeedbackServices : IFeedbackServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FeedbackServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        

        public async Task<ErrorOr<Created>> AddInstructorFeedback(Guid studentId, Guid InstructorId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;
            

            var getInstructors = await unitOfWork.UserRepository.getUserByIdAsync(InstructorId);
            if (getInstructors == null || getInstructors.role.ToLower() != "instructor") return ErrorInstructor.NotFound;
         

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "instructor-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.InstructorId = InstructorId;
            await unitOfWork.FeedbackRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }


        public async Task<ErrorOr<Created>> AddCourseFeedback(Guid studentId, Guid courseId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;

            var getCourse = await unitOfWork.CourseRepository.GetAllCoursesForStudentAsync(studentId);
            if (!getCourse.Any(x => x.courseId == courseId))
                return ErrorCourse.UnavailableCourse;

            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "course-feedback";
            feddbackMapper.StudentId = studentId;
            feddbackMapper.CourseId = courseId;
            await unitOfWork.FeedbackRepository.addFeedbackAsync(feddbackMapper);
            var success = await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddGeneralFeedback(Guid studentId, FeedbackDTO Feedback)
        {
            var getStudent = await unitOfWork.StudentRepository.getStudentByIdAsync(studentId);
            if (getStudent == null) return ErrorStudent.NotFound;
            var feddbackMapper = mapper.Map<FeedbackDTO, Feedback>(Feedback);
            feddbackMapper.type = "general-feedback";
            feddbackMapper.StudentId = studentId;
            await unitOfWork.FeedbackRepository.addFeedbackAsync(feddbackMapper);
            await unitOfWork.StudentRepository.saveAsync();
            return Result.Created;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllGeneralFeedback()
        {
            var getFeedback = await unitOfWork.FeedbackRepository.GetFeedbacksByTypeAsync("general-feedback" );
           
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            foreach (var feedback in feedbackMapper)
            {
                if (feedback.imageUrl != null)
                {
                    feedback.imageUrl = await unitOfWork.FileRepository.GetFileUrl(feedback.imageUrl);
                }
            }
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllInstructorFeedback(Guid? instructorId)
        {
            var getFeedback = await unitOfWork.FeedbackRepository.GetFeedbacksByTypeAsync("instructor-feedback",instructorId);
           
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            foreach (var feedback in feedbackMapper)
            {
                if (feedback.imageUrl != null)
                {
                    feedback.imageUrl = await unitOfWork.FileRepository.GetFileUrl(feedback.imageUrl);
                }
            }
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllCourseFeedback(Guid? courseId)
        {
            var getFeedback = await unitOfWork.FeedbackRepository.GetFeedbacksByTypeAsync("course-feedback" ,null, courseId);
            
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<FeedbackForRetriveDTO>>(getFeedback);
            foreach (var feedback in feedbackMapper)
            {
                if (feedback.imageUrl != null)
                {
                    feedback.imageUrl = await unitOfWork.FileRepository.GetFileUrl(feedback.imageUrl);
                }
            }
            return feedbackMapper;
        }

        public async Task<IReadOnlyList<AllFeedbackForRetriveDTO>> GetAllFeedback()
        {
            var getFeedback = await unitOfWork.FeedbackRepository.GetAllFeedbacksAsync();
          
            var feedbackMapper = mapper.Map<IReadOnlyList<Feedback>, IReadOnlyList<AllFeedbackForRetriveDTO>>(getFeedback);
            foreach (var feedback in feedbackMapper)
            {
                if (feedback.imageUrl != null)
                {
                    feedback.imageUrl = await unitOfWork.FileRepository.GetFileUrl(feedback.imageUrl);
                }
            }
            return feedbackMapper;
        }

        public async Task<ErrorOr<FeedbackForRetriveDTO>> GetFeedbackById(Guid id)
        {
            var getFeedbacks = await unitOfWork.FeedbackRepository.GetAllFeedbacksAsync();
            if (!getFeedbacks.Any(x => x.Id == id)) return ErrorFeedback.NotFound;
            
            var getAFeedback = await unitOfWork.FeedbackRepository.GetFeedbackByIdAsync(id);
            var feedbackMapper = mapper.Map<Feedback, FeedbackForRetriveDTO>(getAFeedback);
            if (feedbackMapper.imageUrl != null)
            {
                feedbackMapper.imageUrl = await unitOfWork.FileRepository.GetFileUrl(feedbackMapper.imageUrl);
            }
            return feedbackMapper;        
    }


    }
}
