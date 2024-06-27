using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorStudent
    {
        public static Error NotFound => Error.NotFound(
            code:"Student.NotFound",
            description:"Student is not found."
            );

    }
}
