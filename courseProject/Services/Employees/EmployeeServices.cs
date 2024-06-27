using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.ServiceErrors;
using courseProject.Services.Instructors;
using courseProject.Services.SubAdmins;
using ErrorOr;

namespace courseProject.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
   

        public EmployeeServices(IUnitOfWork unitOfWork , IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
        }


        public async Task<IReadOnlyList<EmployeeDto>> getAllEmployees()
        {
            var employees = await unitOfWork.UserRepository.GetAllEmployeeAsync();
         

            var mapperemployees= mapper.Map<IReadOnlyList<User>, IReadOnlyList<EmployeeDto>>(employees);
          
         
            var allEmployees = mapperemployees.OrderByDescending(x => x.dateOfAdded).ToList();
            foreach (var employee in allEmployees)
            {
                if (employee.ImageUrl != null)
                {
                    employee.ImageUrl = await unitOfWork.FileRepository.GetFileUrl(employee.ImageUrl);
                }
            }
            return allEmployees;
        }



        public async Task<ErrorOr<Created>> CreateEmployee(EmployeeForCreate employee)
        {
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(employee.email);

            if (!ifUserIsUniqe) return ErrorUser.ExistEmail;

            using (var transaction = await unitOfWork.UserRepository.BeginTransactionAsync())
            {
           
                var userupdated = mapper.Map<User>(employee);
             
                await unitOfWork.UserRepository.createEmployeeAccount(userupdated);
                var success = await unitOfWork.UserRepository.saveAsync();
               



                if (success > 0)
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                return ErrorEmployee.hasError;
            }
        }

        

        public async Task<ErrorOr<object>> GetEmployeeById(Guid id)
        {
            
            // var getSubAdmin = await unitOfWork.SubAdminRepository.GetSubAdminByIdAsync(id);
            //var getInstructor = await unitOfWork.instructorRepositpry.getInstructorByIdAsync(id);
            var UserToGet = await unitOfWork.UserRepository.getUserByIdAsync(id);

            object employee=null;
            if (UserToGet == null && (UserToGet.role.ToLower() == "subadmin"|| UserToGet.role.ToLower() == "main-subadmin")) return ErrorSubAdmin.NotFound;
            
            else if (UserToGet != null && (UserToGet.role.ToLower() == "subadmin" || UserToGet.role.ToLower() == "main-subadmin"))
            {
               
                var mappedEmployee = mapper.Map<User, EmployeeDto>(UserToGet);
               
                employee= mappedEmployee;
            }
            if (UserToGet == null && UserToGet.role.ToLower() == "instructor") return ErrorInstructor.NotFound;

            else if (UserToGet != null && UserToGet.role.ToLower() == "instructor")
            {
                
                var mappedEmployee = mapper.Map<User, EmployeeDto>(UserToGet);
               
                employee= mappedEmployee;
            }
            if (UserToGet == null) return ErrorUser.NotFound;
            return employee;



        }

        public async Task<ErrorOr<Updated>> UpdateEmployeeFromAdmin(Guid employeeId, EmployeeForUpdateDTO employee)
        {
           
            var UserToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(employeeId);

            if (UserToUpdate == null && (UserToUpdate.role.ToLower() == "subadmin" || UserToUpdate.role.ToLower() == "main-subadmin"))
                return ErrorSubAdmin.NotFound;
            if (UserToUpdate == null && UserToUpdate.role.ToLower() == "instructor")
                return ErrorInstructor.NotFound;
       
                var userMapper = mapper.Map(employee, UserToUpdate);
          
                await unitOfWork.UserRepository.UpdateUser(userMapper);
                    var success1 = await unitOfWork.UserRepository.saveAsync();
               
                         return Result.Updated;
               
        }



        public async Task<ErrorOr<Updated>> EditRole(Guid userId, string role)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if (getUser == null) return ErrorUser.NotFound;

            if (getUser.role.ToLower() != "subadmin" && getUser.role.ToLower() != "main-subadmin")
                return ErrorEmployee.InvalidRole;
            if (role.ToLower() != "subadmin" && role.ToLower() != "main-subadmin")
                return ErrorEmployee.InvalidRole;
            var getAllMainSubAdmins = await unitOfWork.UserRepository.getAllMainSubAmdinRole();
            if (getAllMainSubAdmins.Count() >= 1 && role.ToLower() == "main-subadmin")
                return ErrorEmployee.existsMainSub;
            getUser.role = role.ToLower();
            await unitOfWork.UserRepository.editRole(getUser);
            await unitOfWork.UserRepository.saveAsync();

            return Result.Updated;
        }

       
    }
}
