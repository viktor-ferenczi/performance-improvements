namespace Shared.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToYesNo(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}