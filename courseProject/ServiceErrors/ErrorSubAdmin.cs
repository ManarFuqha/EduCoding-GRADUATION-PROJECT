using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorSubAdmin
    {
        public static Error NotFound => Error.NotFound(
          code: "SubAdmin.NotFound",
          description: "SubAdmin is not found."
          );
    }
}
