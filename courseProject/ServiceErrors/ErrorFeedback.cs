using ErrorOr;
namespace courseProject.ServiceErrors
{
    public class ErrorFeedback
    {

        public static Error NotFound => Error.NotFound(
            code:"Feedback.NotFond",
            description:"The Feedback is not found."
            );

    }
}
