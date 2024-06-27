using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using ErrorOr;

namespace courseProject.Services.Employees
{
    public interface IEmployeeServices
    {


        public Task<IReadOnlyList<EmployeeDto>> getAllEmployees();
        public Task<ErrorOr<Created>> CreateEmployee(EmployeeForCreate employee);
        public Task<ErrorOr<Object>> GetEmployeeById(Guid id);
        public Task<ErrorOr<Updated>> UpdateEmployeeFromAdmin(Guid employeeId ,EmployeeForUpdateDTO employee);
        public Task<ErrorOr<Updated>> EditRole(Guid userId, string role);

    }
}
