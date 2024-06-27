using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ILecturesRepository
    {

        public Task<IReadOnlyList<Instructor_Working_Hours>> showifSelectedTimeIsAvilable(TimeSpan startTime, TimeSpan endTime, DateTime date);
        public Task BookLectureAsync(Consultation consultation);
        public Task<IReadOnlyList<StudentConsultations>> GetAllPublicConsultations();
        //   public Task<List<StudentConsultations>> GetStudentInPrivateConsulations(int consultationId);
        public Task<Consultation> GetConsultationById(Guid? consultationId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(Guid studentId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(Guid studentId);
        public Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(Guid StudentId);
        public Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync();

    }
}
