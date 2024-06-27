using courseProject.core.Models;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly projectDbContext dbContext;

        public SubmissionRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IReadOnlyList<Student_Task_Submissions>> GetAllSubmissionsByTaskIdAsync(Guid taskId)
        {
            return await dbContext.Student_Task_Submissions
                .Include(x => x.Student)
                
                .Where(x => x.TaskId == taskId).ToListAsync();
        }


        public async Task SubmitTaskAsync(Student_Task_Submissions student_Task)
        {
            await dbContext.Set<Student_Task_Submissions>().AddAsync(student_Task);
        }

    }
}
