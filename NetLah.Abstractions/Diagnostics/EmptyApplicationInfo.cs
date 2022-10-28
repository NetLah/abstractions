using System.Reflection;

namespace NetLah.Diagnostics;

internal sealed class EmptyApplicationInfo : IApplicationInfo
{
    private readonly ValueStopwatch _uptime = ValueStopwatch.StartNew();

    public static readonly IApplicationInfo Default = new EmptyApplicationInfo();

    public DateTimeOffset StartTime { get; } = DateTimeOffset.Now;

    public TimeSpan Uptime => _uptime.GetElapsedTime();

    public IAssemblyInfo AssemblyInfo { get; } = new AssemblyInfo(typeof(EmptyApplicationInfo).Assembly);

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
