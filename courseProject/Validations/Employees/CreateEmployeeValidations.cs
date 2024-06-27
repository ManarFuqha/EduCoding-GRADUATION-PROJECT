using courseProject.Core.Models.DTO.EmployeesDTO;
using FluentValidation;

namespace courseProject.Validations.Employees
{
    public class CreateEmployeeValidations : AbstractValidator<EmployeeForCreate>
    {
        public CreateEmployeeValidations()
        {

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("The password minimum length is 8 charecter");
            

            RuleFor(x=>x.email)
                .EmailAddress().WithMessage("check the email");
        }
    }
}
