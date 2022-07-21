using System.Reflection;

namespace NetLah.Diagnostics;

public interface IAssemblyInfo
{
    string InformationalVersion { get; }
    string Product { get; }
    string Title { get; }
    string Description { get; }
    AssemblyName Name { get; }
    string ImageRuntimeVersion { get; }
    string FrameworkName { get; }
    DateTimeOffset? BuildDate { get; }

    // format: "1970-01-01T23:59:59+08:00"
    string BuildTimestampLocal { get; }
}
