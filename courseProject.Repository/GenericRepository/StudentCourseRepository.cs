using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;


namespace courseProject.Repository.GenericRepository
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly projectDbContext dbContext;

        public StudentCourseRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task UpdateStudentCourse(StudentCourse studentCourse)
        {
            dbContext.Set<StudentCourse>().Update(studentCourse);
        }





    }
}
