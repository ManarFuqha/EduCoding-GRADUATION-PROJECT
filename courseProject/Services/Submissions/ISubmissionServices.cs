using courseProject.core.Models;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.StudentsDTO;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace courseProject.Services.Submissions
{
    public interface ISubmissionServices
    {

        public Task<ErrorOr<IReadOnlyList<StudentSubmissionDTO>>> GetAllSubmissionForTask(Guid taskId);
        public Task<ErrorOr<Created>> AddTaskSubmission(Guid Studentid, Guid taskid, SubmissionsDTO submissions);

    }
}
