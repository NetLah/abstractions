using System.Globalization;

namespace NetLah.Runtime;

internal static class BuildTimeHelper
{
    public static DateTimeOffset? ParseBuildTime(string value)
        => DateTimeOffset.TryParseExact(value,
            new string[] { "yyyy-MM-ddTHH:mm:ss:fffffffZ", "yyyy-MM-ddTHH:mm:ss" },
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result) ?
        result :
        null;
}
