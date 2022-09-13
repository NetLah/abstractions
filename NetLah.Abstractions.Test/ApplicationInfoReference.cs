using NetLah.Diagnostics;

namespace NetLah.Abstractions.Test;

internal static class ApplicationInfoReference
{
    internal static ApplicationInfo? Instance
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
        set
        {
            var type = typeof(ApplicationInfo);
            if (type.GetField("_instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static) is { } field)
            {
                field.SetValue(null, value);
            }
        }
    }
}
