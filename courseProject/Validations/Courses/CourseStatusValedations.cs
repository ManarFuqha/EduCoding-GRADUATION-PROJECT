using courseProject.Core.Models.DTO.CoursesDTO;
using FluentValidation;

namespace courseProject.Validations.Courses
{
    public class CourseStatusValedations : AbstractValidator<CourseStatusDTO>
    {
        public CourseStatusValedations()
        {

            RuleFor(x => x.Status)
               .NotEmpty().WithMessage("The status is required.")
            .Must(status => new[] { "accredit", "reject", "finish", "start" }.Contains(status))
            .WithMessage("The status must be one of the following values: accredit, reject, finish, start.");

        }
    }
}
