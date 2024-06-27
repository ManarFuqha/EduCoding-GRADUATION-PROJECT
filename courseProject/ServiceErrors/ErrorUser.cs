using ErrorOr;

namespace courseProject.ServiceErrors
{
    public abstract class ErrorUser
    {
        public static Error ExistEmail => Error.Validation(
            code:"Email.Exist",
            description:"Email its Exist"
            );

        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "The User is not found."
            );

        public static Error Unauthorized => Error.Unauthorized(
            code: "User.Unauthorized",
            description: "user is not Unauthorized"
            );

        public static Error UnVarified => Error.Validation(
            code:"User.UnVarefied",
            description:"This email is not varified."
            );

        public static Error InCorrectInput => Error.Validation(
            code:"Login.InCorrect",
            description:"The Email or Password are Incorrect."
            );

        public static Error IncorrectPassword => Error.Validation(
            code:"Password.InCorrect",
            description:"The password is incorrect."
            );

        public static Error hasError => Error.Failure(
          code: "User.Failure",
          description: "There a some Failure occurred"
         );

        public static Error InCorrectCode => Error.Validation(
            code:"Code.InCorrect",
            description:"The Verification Code Is InCorrect."
            );

        public static Error Verified => Error.Validation(
            code:"User.Valedation",
            description: "Your email has already been verified."
            );
    }
}
