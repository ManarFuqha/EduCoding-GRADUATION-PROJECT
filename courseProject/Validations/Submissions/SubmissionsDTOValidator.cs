using courseProject.Core.Models.DTO.MaterialsDTO;
using FluentValidation;

namespace courseProject.Validations.Submissions
{
    public class SubmissionsDTOValidator : AbstractValidator<SubmissionsDTO>
    {
        public SubmissionsDTOValidator()
        {

            RuleFor(submission => submission).Custom((submission, context) =>
            {
                if (string.IsNullOrWhiteSpace(submission.description) && submission.pdf == null)
                {                    
                    context.AddFailure("Either description or pdf must be provided.");
                }
            });

        }
    }
}
