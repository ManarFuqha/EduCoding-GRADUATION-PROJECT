using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorEvent
    {
        public static Error hasError => Error.Failure(
          code: "Event.Failure",
          description: "There a some Failure occurred"
         );

        public static Error NotFound => Error.NotFound(
           code: "Event.NotFound",
           description: "The Event is not found ."
          );
    }
}
