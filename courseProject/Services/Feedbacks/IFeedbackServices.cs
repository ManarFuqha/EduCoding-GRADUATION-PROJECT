using courseProject.Core.Models.DTO.FeedbacksDTO;
using ErrorOr;
namespace courseProject.Services.Feedbacks
{
    public interface IFeedbackServices
    {

        public Task<ErrorOr<Created>> AddInstructorFeedback(Guid studentId, Guid InstructorId, FeedbackDTO Feedback);
        public Task<ErrorOr<Created>> AddCourseFeedback(Guid studentId, Guid courseId, FeedbackDTO Feedback);
        public Task<ErrorOr<Created>> AddGeneralFeedback(Guid studentId, FeedbackDTO Feedback);
        public Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllGeneralFeedback();
        public Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllInstructorFeedback(Guid? instructorId);
        public Task<IReadOnlyList<FeedbackForRetriveDTO>> GetAllCourseFeedback(Guid? courseId);
        public Task<IReadOnlyList<AllFeedbackForRetriveDTO>> GetAllFeedback();
        public Task<ErrorOr<FeedbackForRetriveDTO>> GetFeedbackById(Guid id);

    }
}
