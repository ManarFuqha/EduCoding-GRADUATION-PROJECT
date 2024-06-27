using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorInstructor
    {
        public static Error NotFound => Error.NotFound(
           code: "Instructor.NotFound",
           description: "Instructor is not found."
           );

        public static Error InvalidTime => Error.Validation(
            code:"Time.Invalid",
            description:"The Input Time Is Invalid."
            );

        public static Error NoInstructorAvailable => Error.Validation(
           code: "Instructor.Unavailable",
           description: "There is no instructor available"
           );
    }
}
