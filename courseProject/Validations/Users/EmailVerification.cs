using courseProject.Core.Models.DTO.UsersDTO;
using FluentValidation;

namespace courseProject.Validations.Users
{
    public class EmailVerification : AbstractValidator<EmailDTO>
    {
        public EmailVerification()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("The email is required")
                .EmailAddress().WithMessage("edit your email");
        }
    }
}
