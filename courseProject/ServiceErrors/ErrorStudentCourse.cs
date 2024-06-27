using ErrorOr;
namespace courseProject.ServiceErrors
{
    public class ErrorStudentCourse
    {

        public static Error NotFound => Error.NotFound(
            code:"StudentCourse.NotFound",
            description:"The student is not enroll in this course"
            );

        //public static Error exsist => Error.Validation(
        //    c
        //    );
    }
}
