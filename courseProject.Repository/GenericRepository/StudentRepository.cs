using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class StudentRepository : GenericRepository1<User>, IStudentRepository
    {
        private readonly projectDbContext dbContext;

        public StudentRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            
        }


        // Adds a student consultation to the database.
        public async Task AddInStudentConsulationAsync(StudentConsultations consultation)
        {
            await dbContext.Set<StudentConsultations>().AddAsync(consultation);
        }

        // Enrolls a student in a course.
        public async Task EnrollCourse(StudentCourse studentCourse)
        {
           await dbContext.Set<StudentCourse>().AddAsync(studentCourse);
        }


        // Retrieves all students enrolled in the same course.
        public async Task<IReadOnlyList<User>> GetAllStudentsInTheSameCourseAsync(Guid courseId)
        {
            return await dbContext.users
                          .Where(student => student.studentCourses.Any(sc => sc.courseId == courseId && sc.status.ToLower()== "joind"))
                          .ToListAsync();
        }


        // Retrieves all verified students.
        public async Task<IReadOnlyList<User>> GetAllStudentsAsync()
        {
          
                return await dbContext.users.Where(x => x.IsVerified == true && x.role.ToLower()=="student")
                    .OrderByDescending(x => x.dateOfAdded)
                    .ToListAsync();

           
        }


        // Retrieves a student by their ID.
        public async Task<User> getStudentByIdAsync(Guid? id )
        {          
            return await dbContext.users.Where(x=>x.role.ToLower()=="student").FirstOrDefaultAsync(x => x.UserId == id);
        }


        // Retrieves all students in a specific public consultation.
        public async Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId)
        {
            return await dbContext.StudentConsultations.Where(x=>x.consultationId== consultationId)
                .Where(x => x.consultation.type.ToLower() == "public")
                .Include(x=>x.Student).ToListAsync();
        }




        // Retrieves a student course record by course ID and student ID.
        public async Task<StudentCourse> GetFromStudentCourse(Guid courseId, Guid studentId)
        {
           return await dbContext.studentCourses.FirstOrDefaultAsync(x=>x.courseId==courseId &&  x.StudentId==studentId);
        }


        // Removes a rejected request to join a course.
        public async Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse)
        {
             dbContext.studentCourses.Remove(studentCourse);
        }

       
    }
}
