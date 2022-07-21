namespace NetLah.Common;

public static class TimeZoneLocalHelper
{
    public static TimeZoneInfo GetSingaporeOrCustomTimeZone()
    {
        try
        {
            var result = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => MatchSingaporeTimeZone(tz));

            static bool MatchSingaporeTimeZone(TimeZoneInfo tz) => tz.Id.Contains("Singapore") || tz.DisplayName.Contains("Singapore") || tz.StandardName.Contains("Singapore");

            if (result != null)
            {
                return result;
            }
        }
        catch (Exception)
        {
            // fallback to custom timezone
        }

        var custom = TimeZoneInfo.CreateCustomTimeZone("Singapore Standard Time",
            TimeSpan.FromHours(8),
            "(UTC+08:00) Kuala Lumpur, Singapore",
            "Malay Peninsula Standard Time");

        return custom;
    }
}
