using courseProject.Core.Models.DTO.EventsDTO;
using FluentValidation;

namespace courseProject.Validations.Events
{
    public class EventForCreateValedation : AbstractValidator<EventForCreateDTO>
    {
        public EventForCreateValedation()
        {

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("The event neme is required.");

            RuleFor(x => x.content)
               .NotEmpty().WithMessage("The event content is required.");


            RuleFor(x => x.category)
              .NotEmpty().WithMessage("The event category is required.");

            RuleFor(x => x.SubAdminId)
                .NotEmpty().WithMessage("The subAdmin id is required.");


        }
    }
}
