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
public sealed class ApplicationInfo : IAssemblyInfo
{
    private static readonly object _staticSyncRoot = new();

    public static ApplicationInfo? Instance { get; private set; }

    /// <summary>
    /// Initialize single instance of ApplicationInfo
    /// </summary>
    /// <param name="assembly">Provide null for Assembly.GetEntryAssembly()</param>
    /// <returns></returns>
    public static ApplicationInfo Initialize(Assembly? assembly)
    {
        lock (_staticSyncRoot)
        {
            if (Instance is { } instance)
            {
                throw new InvalidOperationException($"ApplicationInfo is already initialized with assembly: {instance.AssemblyInfo.Name.FullName}");
            }
            Instance = new ApplicationInfo(assembly ?? Assembly.GetEntryAssembly());
        }
        return Instance;
    }

    private ApplicationInfo(Assembly assembly) => AssemblyInfo = new AssemblyInfo(assembly);

    public IAssemblyInfo AssemblyInfo { get; }

    public string InformationalVersion => AssemblyInfo.InformationalVersion;

    public string Product => AssemblyInfo.Product;

    public string Title => AssemblyInfo.Title;

    public string Description => AssemblyInfo.Description;

    public AssemblyName Name => AssemblyInfo.Name;

    public string ImageRuntimeVersion => AssemblyInfo.ImageRuntimeVersion;

    public string FrameworkName => AssemblyInfo.FrameworkName;

    public DateTimeOffset? BuildDate => AssemblyInfo.BuildDate;

    public string BuildTimestampLocal => AssemblyInfo.BuildTimestampLocal;
}
