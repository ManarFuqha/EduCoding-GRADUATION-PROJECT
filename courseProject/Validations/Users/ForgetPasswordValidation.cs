using courseProject.Core.Models.DTO.UsersDTO;
using FluentValidation;

namespace courseProject.Validations.Users
{
    public class ForgetPasswordValidation : AbstractValidator<ForgetPasswordDTO>

    {
        public ForgetPasswordValidation()
        {
            RuleFor(x => x.password)
                .NotEmpty().WithMessage(" Password is required")
                .MinimumLength(8).WithMessage("The password minimum length is 8 charecter")
                .MaximumLength(25).WithMessage("The password is too long");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.password).WithMessage("Confirm must equal to password");


        }
    }
}
