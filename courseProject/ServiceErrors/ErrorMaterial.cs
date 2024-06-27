using ErrorOr;
namespace courseProject.ServiceErrors
{
    public class ErrorMaterial
    {

        public static Error NotFound => Error.NotFound(
            code:"Material.NotFound",
            description:"The Material is not found."
            );

    }
}
