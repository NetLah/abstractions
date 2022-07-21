using NetLah.Runtime;
using System.Reflection;
using System.Runtime.Versioning;

namespace NetLah.Diagnostics;

public class AssemblyInfo : IAssemblyInfo
{
    private readonly Assembly _assembly;
    private readonly Lazy<string> _informationalVersion;
    private readonly Lazy<string> _product;
    private readonly Lazy<string> _title;
    private readonly Lazy<string> _description;
    private readonly Lazy<string> _imageRuntimeVersion;
    private readonly Lazy<string> _frameworkName;
    private readonly Lazy<DateTimeOffset?> _buildDate;
    private readonly Lazy<string> _buildTimestampLocal;

    public AssemblyInfo(Assembly assembly)
    {
        _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        _informationalVersion = new Lazy<string>(() => _assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "_informationalVersion");
        _product = new Lazy<string>(() => _assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "_product");
        _title = new Lazy<string>(() => _assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "_title");
        _description = new Lazy<string>(() => _assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "_description");
        _imageRuntimeVersion = new Lazy<string>(() => _assembly.ImageRuntimeVersion ?? "_imageRuntimeVersion");
        _frameworkName = new Lazy<string>(() => _assembly.GetCustomAttribute<TargetFrameworkAttribute>().FrameworkName ?? "_frameworkName");
        _buildDate = new Lazy<DateTimeOffset?>(() => _assembly.GetCustomAttribute<AssemblyBuildDateAttribute>()?.DateTime);
        _buildTimestampLocal = new Lazy<string>(() => GetTimestampString(BuildDate ?? DateTimeOffset.Now, TimeZoneInfo.Local));
    }

    private static string GetTimestampString(DateTimeOffset dateTimeOffset, TimeZoneInfo timeZoneInfo)
    {
        var local = TimeZoneInfo.ConvertTime(dateTimeOffset, timeZoneInfo);
        var localString = local.ToString("yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture);
        return localString;
    }

    public string InformationalVersion => _informationalVersion.Value;

    public string Product => _product.Value;

    public string Title => _title.Value;

    public string Description => _description.Value;

    public AssemblyName Name => _assembly.GetName();

    public string ImageRuntimeVersion => _imageRuntimeVersion.Value;

    public string FrameworkName => _frameworkName.Value;

    public DateTimeOffset? BuildDate => _buildDate.Value;

    // format: "1970-01-01T23:59:59+08:00"
    public string BuildTimestampLocal => _buildTimestampLocal.Value;
}
