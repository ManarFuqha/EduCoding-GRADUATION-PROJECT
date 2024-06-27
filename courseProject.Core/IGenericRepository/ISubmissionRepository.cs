using courseProject.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface ISubmissionRepository
    {

        public Task<IReadOnlyList<Student_Task_Submissions>> GetAllSubmissionsByTaskIdAsync(Guid taskId);
        public Task SubmitTaskAsync(Student_Task_Submissions student_Task);

    }
}
