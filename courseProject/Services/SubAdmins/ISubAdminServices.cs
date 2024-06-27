using courseProject.Core.Models;
using ErrorOr;

namespace courseProject.Services.SubAdmins
{
    public interface ISubAdminServices
    {
        public Task<IReadOnlyList<User>> GetAllSubAdmins();
        public Task<ErrorOr<User>> getSubAdminById(Guid subAdminId);
    }
}
