using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.ServiceErrors;
using ErrorOr;

namespace courseProject.Services.SubAdmins
{
    public class SubAdminServices : ISubAdminServices
    {
        private readonly IUnitOfWork unitOfWork;

        public SubAdminServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<User>> GetAllSubAdmins()
        {
            return await unitOfWork.UserRepository.GetAllEmployeeAsync();
        }
        public async Task<ErrorOr<User>> getSubAdminById(Guid subAdminId)
        {

            var SubAdminFound = await unitOfWork.UserRepository.ViewProfileAsync(subAdminId, "subadmin");
            if (SubAdminFound == null) return ErrorSubAdmin.NotFound;
            return await unitOfWork.UserRepository.getUserByIdAsync(subAdminId);
        }
    }
}
