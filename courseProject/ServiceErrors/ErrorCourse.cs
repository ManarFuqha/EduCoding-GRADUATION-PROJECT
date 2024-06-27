using ErrorOr;

namespace courseProject.ServiceErrors
{
    public class ErrorCourse
    {
        public static Error NotFound => Error.NotFound(
            code: "Course.NotFound",
            description: "Course is not found."
            );

        public static Error InvalidCourseId => Error.Validation(
            code: "CourseId.Invalid",
            description: "Course id is less than 0"
            );

        public static Error NoContent => Error.Validation(
            code: "CourseId.NoContent",
            description: "There is no content"
            );

        public static Error hasError => Error.Failure(
    code: "Course.Failure",
    description: "There a some Failure occurred"
    );


        public static Error fullCourse => Error.Validation(
            code:"Course.Full",
            description:"The Course Is Full."
            );

        public static Error UnavailableCourse => Error.Validation(
            code: "Course.Unavailable",
            description: "The course is Unavailable."
            );

    }
}
