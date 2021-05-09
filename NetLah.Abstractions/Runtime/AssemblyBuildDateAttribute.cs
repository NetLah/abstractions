using System;
using System.Globalization;

namespace NetLah.Runtime
{
    // https://www.meziantou.net/2018/09/24/getting-the-date-of-build-of-a-net-assembly-at-runtime

    /// <summary>
    /// Store assembly build date time
    /// <code>
    ///   <ItemGroup>
    ///     <AssemblyAttribute Include="NetLah.Runtime.AssemblyBuildDateAttribute">
    ///       <_Parameter1>$([System.DateTime]::UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"))</_Parameter1>
    ///     </AssemblyAttribute>
    ///   </ItemGroup>
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class AssemblyBuildDateAttribute : Attribute
    {
        public AssemblyBuildDateAttribute(string value)
            => DateTime = DateTimeOffset.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result)
                ? result : DateTimeOffset.UtcNow;

        public DateTimeOffset DateTime { get; }
    }
}
