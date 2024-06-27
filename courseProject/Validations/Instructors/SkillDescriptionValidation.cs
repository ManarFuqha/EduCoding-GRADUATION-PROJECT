using courseProject.Core.Models.DTO.InstructorsDTO;
using FluentValidation;

namespace courseProject.Validations.Instructors
{
    public class SkillDescriptionValidation : AbstractValidator<SkillDescriptionDTO>
    {
        public SkillDescriptionValidation()
        {

            RuleFor(x => x.skillDescription)
                .MinimumLength(5).WithMessage("Description is too short");

        }
    }
}
