using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;


namespace courseProject.Repository.GenericRepository
{
    public class LecturesRepository : ILecturesRepository
    {
        private readonly projectDbContext dbContext;

        public LecturesRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



       // Checks if the selected time is available for any instructor on a given date.
        public async Task<IReadOnlyList<Instructor_Working_Hours>> showifSelectedTimeIsAvilable(TimeSpan startTime, TimeSpan endTime, DateTime date)
        {
            return await dbContext.Instructor_Working_Hours.Include(x => x.instructor).ThenInclude(x => x.consultations)
                 .Where(x => x.day == date.DayOfWeek)
                 .Where(x => x.startTime <= startTime && x.endTime >= endTime)
                 .Where(x => !x.instructor.consultations.Any(y =>
         y.date == date.Date && (
             (startTime >= y.startTime && startTime < y.endTime) ||
             (endTime > y.startTime && endTime <= y.endTime) ||
             (startTime <= y.startTime && endTime >= y.endTime)
         )))
     .ToListAsync();
                           
        }


        public async Task BookLectureAsync(Consultation consultation)
        {
            await dbContext.Set<Consultation>().AddAsync(consultation);
        }


        //Retrieves all lectures associated with a specific student by their ID.
        public async Task<IReadOnlyList<StudentConsultations>> GetAllLectureByStudentIdAsync(Guid StudentId)
        {
            return await dbContext.StudentConsultations.Include(x => x.consultation.student)
                .Include(x => x.consultation.student)
                                                       .Include(x => x.Student)
                                                       .Where(x => x.StudentId == StudentId).ToListAsync();
        }

        //Retrieves all public consultations
        public async Task<IReadOnlyList<Consultation>> GetAllPublicConsultationsAsync()
        {
            return await dbContext.consultations.Where(x => x.type.ToLower() == "public").ToListAsync();
        }

        //Retrieves all student consultations for public consultations.
        public async Task<IReadOnlyList<StudentConsultations>> GetAllPublicConsultations()
        {
            return await dbContext.StudentConsultations
                .Where(x => x.consultation.type.ToLower() == "public")
                                                       .Include(x => x.consultation)
                                                            
                                                               .ThenInclude(x => x.student)
                                                       .Include(x => x.Student)
                                                       
                                                       .ToListAsync();
        }

        // get all private consultations , where student by student id does not enroll in it 
        public async Task<IReadOnlyList<StudentConsultations>> GetAllOtherPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x => x.consultation.type.ToLower() == "private")
                                                       .Where(x => x.StudentId != studentId)
                                                       .Include(x => x.consultation)
                                                             
                                                               .ThenInclude(x => x.student)
                                                       .Include(x => x.Student)
                                                               
                                                       .ToListAsync();
        }


        //Retrieves all booked private consultations for a specific student by their ID.
        public async Task<IReadOnlyList<StudentConsultations>> GetAllBookedPrivateConsultationsAsync(Guid studentId)
        {
            return await dbContext.StudentConsultations.Where(x => x.consultation.type.ToLower() == "private")
                                                       .Where(x => x.StudentId == studentId)
                                                       .Include(x => x.consultation)
                                                            
                                                               .ThenInclude(x => x.student)
                                                       .Include(x => x.Student)
                                                               
                                                       .ToListAsync();
        }


        //Retrieves a specific consultation by its ID.
        public async Task<Consultation> GetConsultationById(Guid? consultationId)
        {
            return await dbContext.consultations.Include(x => x.student)
                 .FirstOrDefaultAsync(x => x.Id == consultationId);
        }


    }
}
