using courseProject.Core.Models.DTO.UsersDTO;
using FluentValidation;

namespace courseProject.Validations.Users
{
    public class ChengePasswordValidation:AbstractValidator<ChengePasswordDTO>

    {
        public ChengePasswordValidation()
        {
            RuleFor(x => x.Newpassword)
                .NotEmpty().WithMessage("New Password is required")
                .MinimumLength(8).WithMessage("The password minimum length is 8 charecter")
                .MaximumLength(25).WithMessage("The password is too long");

           

        }
    }
}
