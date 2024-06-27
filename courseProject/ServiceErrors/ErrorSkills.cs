using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorSkills
    {
        public static Error NotFound => Error.NotFound(
            code: "Skills.NotFound",
            description: "skill is not found."          
            );

        public static Error ChapterNameExists => Error.Validation(
            code: "SkillName.Duplication",
            description: "Skill name already exists."
        );

        public static Error NoContent => Error.Validation(
            code: "Skills.NoContent",
            description: "There is no content"
            );

        public static Error NotHasSkill => Error.Validation(
            code: "Skills.NotFound",
            description: "Instructor is not has a skill"
            );
    }
}
