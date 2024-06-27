using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly projectDbContext dbContext;

        public RequestRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IReadOnlyList<Consultation>> GetAllConsultationRequestByInstructorIdAsync(Guid instructorId)
        {
            return await dbContext.consultations.Include(x => x.student).Where(x => x.InstructorId == instructorId).ToListAsync();
        }

        public async Task CreateRequest(Request request)
        {
            await dbContext.Set<Request>().AddAsync(request);
        }

        public async Task<IReadOnlyList<Request>> GerAllCoursesRequestAsync()
        {
            return await dbContext.requests.Include(x => x.student).Where(x => x.satus == "custom-course")
                .OrderByDescending(x => x.date)
                .ToListAsync();
        }


        public async Task<Request> GerCourseRequestByIdAsync(Guid id)
        {
            return await dbContext.requests.Include(x => x.student).Where(x => x.satus == "custom-course").FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IReadOnlyList<StudentCourse>> getAllRequestToJoindCourseAsync()
        {
            return await dbContext.studentCourses.Include(x => x.Student)

                                                 .Include(x => x.Course)
                                                 .Where(x => x.status.ToLower() == "waiting")
                                                 .OrderByDescending(x=>x.EnrollDate)
                                                 .ToListAsync();
        }


    }
}
