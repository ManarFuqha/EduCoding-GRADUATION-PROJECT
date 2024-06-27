using courseProject.Core.Models.DTO.EventsDTO;
using FluentValidation;

namespace courseProject.Validations.Events
{
    public class EventStatusValidations : AbstractValidator<EventStatusDTO>
    {
        public EventStatusValidations()
        {

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("The event status is required.")
                .Must(status => new[] { "accredit", "reject" }.Contains(status))
                .WithMessage("The event status must be accredit or reject");

        }
    }
}
