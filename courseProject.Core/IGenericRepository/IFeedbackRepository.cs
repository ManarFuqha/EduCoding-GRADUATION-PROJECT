using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IFeedbackRepository
    {

        public Task addFeedbackAsync(Feedback feedback);
        public Task<IReadOnlyList<Feedback>> GetAllFeedbacksAsync();
        public Task<IReadOnlyList<Feedback>> GetFeedbacksByTypeAsync(string type, Guid? instructorId = null, Guid? courseId = null);
        public Task<Feedback> GetFeedbackByIdAsync(Guid id);
     
    }
}
