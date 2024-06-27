namespace courseProject.Common
{
    public class IsNotDefaultClassForMapping
    {
        public static bool IsNotDefault(object srcMember)
        {
            if (srcMember is int intValue)
            {
                return intValue != default;
            }
            else if (srcMember is double doubleValue)
            {
                return doubleValue != default;
            }
            else if (srcMember is Guid GuidValue)
            {
                return GuidValue != default;
            }
            return srcMember != null;
        }
    }
}
