using courseProject.Core.Models.DTO.RegisterDTO;
using FluentValidation;
using System.Data;

namespace courseProject.Validations.Registeration
{
    public class RegisterValidation : AbstractValidator<RegistrationRequestDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.email)
              .EmailAddress().WithMessage("check the email");
            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("The password minimum length is 8 charecter")
                .MaximumLength(25).WithMessage("The password is too long");


            RuleFor(x => x.ConfirmPassword).Equal(x => x.password).WithMessage("Confirm must equal to password");
           
        }



    }
}
