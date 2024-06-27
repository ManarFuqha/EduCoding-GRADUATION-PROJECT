using courseProject.Core.Models.DTO.UsersDTO;
using FluentValidation;

namespace courseProject.Validations.Users
{
    public class ProfileValidation : AbstractValidator<ProfileDTO>
    {
        public ProfileValidation()
        {

            RuleFor(x => x.image)
                .Must(HaveValidImageExtension)
            .WithMessage("Image must have a .jpg , .png or .jpeg extension.");
        }



        private bool HaveValidImageExtension(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return true;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

    }
}

