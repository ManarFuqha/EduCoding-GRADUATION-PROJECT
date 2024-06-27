using courseProject.Core.Models.DTO.LecturesDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Services.Lectures
{
    public interface ILectureServices
    {

        public Task<ErrorOr<IReadOnlyList<LecturesForRetriveDTO>>> GetAllLecturesByInstructorId(Guid instructorId);
        public Task<ErrorOr<Created>> BookALecture(Guid studentId, DateTime date, string startTime, string endTime, BookALectureDTO bookALecture);
        public Task<ErrorOr<Created>> JoinToPublicLecture(Guid StudentId, Guid ConsultaionId);
        public Task<ErrorOr<IReadOnlyList<PublicLectureForRetriveDTO>>> GetAllConsultations(Guid studentId);
        public Task<ErrorOr<LecturesForRetriveDTO>> GetConsultationById(Guid consultationId);

    }
}
