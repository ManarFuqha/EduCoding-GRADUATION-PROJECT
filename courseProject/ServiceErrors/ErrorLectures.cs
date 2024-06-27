using courseProject.Core.Models;
using ErrorOr;
namespace courseProject.ServiceErrors
{
    public class ErrorLectures
    {

        public static Error InvalidTime => Error.Validation(
            code:"Time.Invalid",
            description:"Invalid Time Formate."
            );

        public static Error limitationTime => Error.Validation(
            code:"Time.Limit",
            description: "The duration is less or more than the permitted period."
            );

        public static Error GraterTime => Error.Validation(
            code:"Time.grater",
            description: "The start time is greater than the end time"
            );


        public static Error NotFound => Error.NotFound(
            code:"Lecture.NotFound", 
            description:"Lecture is not found."
            );
    }
}
