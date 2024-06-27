using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IRequestRepository
    {

        public Task<IReadOnlyList<Consultation>> GetAllConsultationRequestByInstructorIdAsync(Guid instructorId);
        public Task CreateRequest(Request model);
        public Task<IReadOnlyList<Request>> GerAllCoursesRequestAsync();
        public Task<Request> GerCourseRequestByIdAsync(Guid id);
        public Task<IReadOnlyList<StudentCourse>> getAllRequestToJoindCourseAsync();

    }
}
