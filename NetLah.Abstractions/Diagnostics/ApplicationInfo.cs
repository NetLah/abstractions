using System;
using System.Reflection;

namespace NetLah.Diagnostics
{
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

        public static ApplicationInfo Instance { get; private set; }

        /// <summary>
        /// Initialize single instance of ApplicationInfo
        /// </summary>
        /// <param name="assembly">Provide null for Assembly.GetEntryAssembly()</param>
        /// <returns></returns>
        public static ApplicationInfo Initialize(Assembly assembly)
        {
            lock (_staticSyncRoot)
            {
                if (Instance != null)
                {
                    throw new InvalidOperationException($"ApplicationInfo already init with assembly: {Instance.Assembly.Name.FullName}");
                }
                Instance = new ApplicationInfo(assembly ?? System.Reflection.Assembly.GetEntryAssembly());
            }
            return Instance;
        }

        private ApplicationInfo(Assembly assembly) => Assembly = new AssemblyInfo(assembly);

        public AssemblyInfo Assembly { get; }

        public string InformationalVersion => Assembly.InformationalVersion;

        public string Product => Assembly.Product;

        public string Title => Assembly.Title;

        public string Description => Assembly.Description;

        public AssemblyName Name => Assembly.Name;

        public string ImageRuntimeVersion => Assembly.ImageRuntimeVersion;

        public string FrameworkName => Assembly.FrameworkName;

        public DateTimeOffset BuildDate => Assembly.BuildDate;

        public string BuildTimestampLocal => Assembly.BuildTimestampLocal;
    }
}
