using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorEmployee
    {
        public static Error hasError => Error.Failure(
           code: "Employee.Failure",
           description: "There a some Failure occurred"
            );

        public static Error NotFound => Error.NotFound(
          code: "Employee.NotFound",
          description: "Employee is not found."
          );

        public static Error InvalidRole => Error.Validation(
            code:"Role.Invalid",
            description:"The current or new role is invalid."
            );

        public static Error existsMainSub => Error.Validation(
            code:"Main-SubAdmin.Dublication",
            description: "The Main-SubAdmin role is exists."
            );
    }
}
