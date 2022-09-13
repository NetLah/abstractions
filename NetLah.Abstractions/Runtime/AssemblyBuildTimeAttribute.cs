namespace NetLah.Runtime;

// https://www.meziantou.net/2018/09/24/getting-the-date-of-build-of-a-net-assembly-at-runtime

/// <summary>
/// Store assembly build date time
/// <code>
///   <ItemGroup>
///     <AssemblyAttribute Include="NetLah.Runtime.AssemblyBuildTimeAttribute">
///       <_Parameter1>$([System.DateTime]::UtcNow.ToString("O"))</_Parameter1>
///     </AssemblyAttribute>
///   </ItemGroup>
/// </code>
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class AssemblyBuildTimeAttribute : Attribute
{
    public AssemblyBuildTimeAttribute(string value) => DateTime = BuildTimeHelper.ParseBuildTime(value);

    public DateTimeOffset? DateTime { get; }
}
