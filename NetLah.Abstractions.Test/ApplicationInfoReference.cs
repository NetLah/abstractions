using NetLah.Diagnostics;

namespace NetLah.Abstractions.Test;

internal static class ApplicationInfoReference
{
    public static ApplicationInfo? Instance
    {
        get
        {
            var type = typeof(ApplicationInfo);
            if (type.GetField("_instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static) is { } field)
            {
                return field.GetValue(null) as ApplicationInfo;
            }
            return default;
        }

        private set
        {
            var type = typeof(ApplicationInfo);
            if (type.GetField("_instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static) is { } field)
            {
                field.SetValue(null, value);
            }
        }
    }

    public static void Reset() => Instance = null;

    internal static void SetAny()
    {
        if (Instance == null)
        {
            ApplicationInfo.Initialize(typeof(BuildTimeHelperTest).Assembly);
        }
    }
}
