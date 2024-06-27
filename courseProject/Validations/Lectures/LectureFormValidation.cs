using courseProject.Common;
using courseProject.Core.Models.DTO.LecturesDTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace courseProject.Validations.Lectures
{
    public class LectureFormValidation : AbstractValidator<LectureFormDTO>
    {
        public LectureFormValidation()
        {

            RuleFor(x => x.skillId)
                .NotEmpty().WithMessage("Skill is is required.");

            RuleFor(x => x.startTime)
                .NotEmpty().WithMessage("Start Time is requried.")
                .Must(CommonClass.IsValidTimeFormat).WithMessage("Start time is Invalid Formate.");

            RuleFor(x => x.endTime)
                .NotEmpty().WithMessage("End Time is requried.")
                .Must(CommonClass.IsValidTimeFormat).WithMessage("End time is Invalid Formate.");

            RuleFor(x => x.date)
                .NotEmpty().WithMessage("The date is required.");
              



        }
     

    }
}
