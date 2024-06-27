using courseProject.Core.Models;
using courseProject.Core.Models.DTO.CoursesDTO;
using FluentValidation;
using static System.Net.Mime.MediaTypeNames;

namespace courseProject.Validations.Courses
{
    public class CreateCourseValidations : AbstractValidator<CourseForCreateDTO>
    {
        public CreateCourseValidations()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Course name is required.");

            RuleFor(x => x.description)
                .NotEmpty().WithMessage("Course description is reqired.")
                .MinimumLength(4).WithMessage("Course description is too short");

            RuleFor(x => x.price)
                .NotEmpty().WithMessage("Course price is required.");

            RuleFor(x => x.category)
                .NotEmpty().WithMessage("Course category is required.");
            RuleFor(x => x.image)
                
                .Must(HaveValidImageExtension)
            .WithMessage("Image must have a .jpg , .png or .jpeg extension.");

            RuleFor(x => x.startDate)
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("The start date must grater then the date now or equal to it");





        }



        private bool HaveValidImageExtension(IFormFile image)
        {
            if (image == null)
            {
                return true;
            }
            if (image.Length == 0) return false;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}

