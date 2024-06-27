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

        

        public async Task AddInStudentConsulationAsync(StudentConsultations consultation)
        {
            await dbContext.Set<StudentConsultations>().AddAsync(consultation);
        }

        public async Task CreateStudentAccountAsync(User student)
        {
          await  dbContext.Set<User>().AddAsync(student);
        }

       

        public async Task EnrollCourse(StudentCourse studentCourse)
        {
           await dbContext.Set<StudentCourse>().AddAsync(studentCourse);
        }

       

        public async Task<IReadOnlyList<User>> GetAllStudentsInTheSameCourseAsync(Guid courseId)
        {
            return await dbContext.users
                          .Where(student => student.studentCourses.Any(sc => sc.courseId == courseId && sc.status.ToLower()== "joind"))
                          .ToListAsync();
        }



        public async Task<IReadOnlyList<User>> GetAllStudentsAsync()
        {
          
                return await dbContext.users.Where(x => x.IsVerified == true && x.role.ToLower()=="student")
                    .OrderByDescending(x => x.dateOfAdded)
                    .ToListAsync();

           
        }



        public async Task<User> getStudentByIdAsync(Guid? id )
        {          
            return await dbContext.users.Where(x=>x.role.ToLower()=="student").FirstOrDefaultAsync(x => x.UserId == id);
        }

       
        public async Task<List<StudentConsultations>> GetAllStudentsInPublicConsulations(Guid consultationId)
        {
            return await dbContext.StudentConsultations.Where(x=>x.consultationId== consultationId)
                .Where(x => x.consultation.type.ToLower() == "public")
                .Include(x=>x.Student).ToListAsync();
        }

       

      

        public async Task<StudentCourse> GetFromStudentCourse(Guid courseId, Guid studentId)
        {
           return await dbContext.studentCourses.FirstOrDefaultAsync(x=>x.courseId==courseId &&  x.StudentId==studentId);
        }

        public async Task RemoveTheRejectedRequestToJoinCourse(StudentCourse studentCourse)
        {
             dbContext.studentCourses.Remove(studentCourse);
        }

       
    }
}
