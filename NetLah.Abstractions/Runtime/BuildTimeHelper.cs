using System.Globalization;
using System.Reflection;

namespace NetLah.Runtime;

// https://www.meziantou.net/getting-the-date-of-build-of-a-dotnet-assembly-at-runtime.htm
// https://www.meziantou.net/2018/09/24/getting-the-date-of-build-of-a-net-assembly-at-runtime

/// <summary>
/// Store assembly build date time
/// <code>
///   <ItemGroup>
///     <AssemblyAttribute Include="System.Reflection.AssemblyMetadataAttribute">
///       <_Parameter1>BuildTime</_Parameter1>
///       <_Parameter2>$([System.DateTime]::UtcNow.ToString("O"))</_Parameter2>
///     </AssemblyAttribute>
///   </ItemGroup>
/// </code>
/// </summary>

internal static class BuildTimeHelper
{
    public static DateTimeOffset? ParseBuildTime(string? value)
        => !string.IsNullOrEmpty(value) &&
            DateTimeOffset.TryParseExact(value,
                new string[] { "yyyy-MM-ddTHH:mm:ss:fffffffZ", "yyyy-MM-ddTHH:mm:ss" },
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result) ?
            result :
            null;

    public static DateTimeOffset? ParseBuildTime(IEnumerable<AssemblyMetadataAttribute> attributes)
        => ParseBuildTime(attributes?.FirstOrDefault(a => a.Key == "BuildTime"));

    public static DateTimeOffset? ParseBuildTime(AssemblyMetadataAttribute? attribute)
        => ParseBuildTime(attribute?.Value);
}
