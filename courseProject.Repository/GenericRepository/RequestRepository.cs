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



        // Retrieves all consultation requests by instructor ID.
        public async Task<IReadOnlyList<Consultation>> GetAllConsultationRequestByInstructorIdAsync(Guid instructorId)
        {
            return await dbContext.consultations.Include(x => x.student).Where(x => x.InstructorId == instructorId).ToListAsync();
        }


        // Creates a new request and adds it to the database.
        public async Task CreateRequest(Request request)
        {
            await dbContext.Set<Request>().AddAsync(request);
        }


        // Retrieves all custom course requests.
        public async Task<IReadOnlyList<Request>> GerAllCoursesRequestAsync()
        {
            return await dbContext.requests.Include(x => x.student).Where(x => x.satus == "custom-course")
                .OrderByDescending(x => x.date)
                .ToListAsync();
        }

        // Retrieves a custom course request by its ID.
        public async Task<Request> GerCourseRequestByIdAsync(Guid id)
        {
            return await dbContext.requests.Include(x => x.student).Where(x => x.satus == "custom-course").FirstOrDefaultAsync(x => x.Id == id);
        }

        // Retrieves all requests to join a course with status 'waiting'.
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
