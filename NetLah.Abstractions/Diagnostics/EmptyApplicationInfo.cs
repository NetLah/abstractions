using System.Reflection;

namespace NetLah.Diagnostics;

internal sealed class EmptyApplicationInfo : IAssemblyInfo
{
    public static readonly IAssemblyInfo Default = new EmptyApplicationInfo();

    public string InformationalVersion => "InformationalVersion";

    public string Product => "Product";

    public string Title => "Title";

    public string Description => "Description";

    public AssemblyName Name { get; } = new AssemblyName();

    public string ImageRuntimeVersion => "ImageRuntimeVersion";

    public string FrameworkName => "FrameworkName";

    public DateTimeOffset? BuildTime => DateTimeOffset.Now;

    public DateTimeOffset? BuildDate => DateTimeOffset.Now;

    public string BuildTimestampLocal => "BuildTimestampLocal";
}
