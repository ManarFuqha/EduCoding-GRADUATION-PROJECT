using courseProject.Core.Models.DTO.Reports;
using FluentValidation;

namespace courseProject.Validations.Reports
{
    public class ReportValidation : AbstractValidator<ReportDTO>
    {
        public ReportValidation()
        {

            RuleFor(r => r.data)
             .NotEmpty().WithMessage("Data field is required.")
             .Must(value => IsValidDataType(value)).WithMessage("Data must be 'employee', 'student', or 'course'.");
        }

        private bool IsValidDataType(string data)
        {
            var validTypes = new[] { "employee", "student", "course" };
            return validTypes.Contains(data.ToLower());
        }
    }
}
