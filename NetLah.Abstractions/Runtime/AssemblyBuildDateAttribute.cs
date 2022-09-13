namespace NetLah.Runtime;

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
[Obsolete("Use AssemblyBuildTimeAttribute")]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class AssemblyBuildDateAttribute : Attribute
{
    public AssemblyBuildDateAttribute(string value) => DateTime = BuildTimeHelper.ParseBuildTime(value);

    public DateTimeOffset? DateTime { get; }
}
