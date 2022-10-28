using System.Reflection;

namespace NetLah.Diagnostics;

/// <summary>
/// ApplicationInfo for entry assembly
/// </summary>
/// <example>
/// Initialize single instance of ApplicationInfo first
/// <code>
/// var appInfo = ApplicationInfo.Initialize(Assembly.GetEntryAssembly());
/// </code>
/// </example>
/// <returns></returns>
public sealed class ApplicationInfo : IApplicationInfo
{
    private static readonly object _staticSyncRoot = new();
    private static ApplicationInfo? _instance;
    private readonly ValueStopwatch _uptime = ValueStopwatch.StartNew();

    public static IApplicationInfo InstanceOrDefault => _instance ?? EmptyApplicationInfo.Default;

    public static IApplicationInfo Instance => _instance ?? TryInitialize(null);

    /// <summary>
    /// Initialize single instance of ApplicationInfo
    /// </summary>
    /// <param name="assembly">Provide null for Assembly.GetEntryAssembly()</param>
    /// <returns></returns>
    public static IApplicationInfo Initialize(Assembly? assembly)
    {
        lock (_staticSyncRoot)
        {
            if (_instance is { } instance)
            {
                throw new InvalidOperationException($"ApplicationInfo is already initialized with assembly: {instance.AssemblyInfo.Name.FullName}");
            }
        }

        return TryInitialize(assembly);
    }

    public static IApplicationInfo TryInitialize(Assembly? assembly)
    {
        if (_instance == null)
        {
            lock (_staticSyncRoot)
            {
                _instance ??= new ApplicationInfo(assembly ?? Assembly.GetEntryAssembly());
            }
        }

        return InstanceOrDefault;
    }

    private ApplicationInfo(Assembly assembly) => AssemblyInfo = new AssemblyInfo(assembly);

    public DateTimeOffset StartTime { get; } = DateTimeOffset.Now;

    public TimeSpan Uptime => _uptime.GetElapsedTime();

    public IAssemblyInfo AssemblyInfo { get; }

    public string InformationalVersion => AssemblyInfo.InformationalVersion;

    public string Product => AssemblyInfo.Product;

    public string Title => AssemblyInfo.Title;

    public string Description => AssemblyInfo.Description;

    public AssemblyName Name => AssemblyInfo.Name;

    public string ImageRuntimeVersion => AssemblyInfo.ImageRuntimeVersion;

    public string FrameworkName => AssemblyInfo.FrameworkName;

    public DateTimeOffset? BuildTime => AssemblyInfo.BuildTime;

    [Obsolete("Use BuildTime property")]
    public DateTimeOffset? BuildDate => AssemblyInfo.BuildTime;

    public string BuildTimestampLocal => AssemblyInfo.BuildTimestampLocal;
}
