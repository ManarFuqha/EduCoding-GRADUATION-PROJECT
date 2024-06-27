using courseProject.Core.Models.DTO.ContactUsDTO;
using FluentValidation;

namespace courseProject.Validations.Contact
{
    public class CreateMessageContactValedations : AbstractValidator<CreateMessageContactDTO>
    {
        public CreateMessageContactValedations()
        {

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("The email is required.")
                .EmailAddress().WithMessage("Add a correct email.");

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("The name is required.");

            RuleFor(x => x.message)
                .NotEmpty().WithMessage("The message is required.");
        }
    }
}
