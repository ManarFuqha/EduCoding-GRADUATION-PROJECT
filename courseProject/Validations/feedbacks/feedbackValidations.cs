using courseProject.Core.Models.DTO.FeedbacksDTO;
using FluentValidation;

namespace courseProject.Validations.feedbacks
{
    public class feedbackValidations : AbstractValidator<FeedbackDTO>
    {
        public feedbackValidations()
        {

            RuleFor(x => x.range)
                .LessThanOrEqualTo(5).WithMessage("The range must be less than or equal to 5.")
                .GreaterThanOrEqualTo(0).WithMessage("The range must be greater than or equal to 0.");

        }
    }
}
