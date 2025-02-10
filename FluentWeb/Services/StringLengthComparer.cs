namespace FluentWeb.Services
{
    public class StringLengthComparer : IComparer<string>
    {
        public static readonly StringLengthComparer Instance = new StringLengthComparer();

        public int Compare(string? x, string? y)
        {
            if (x is null)
            {
                return y is null ? 0 : -1;
            }

            if (y is null)
            {
                return 1;
            }

            return x.Length.CompareTo(y.Length);
        }
    }
    public static class TruncateService
    {
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}
