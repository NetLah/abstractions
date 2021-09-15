using System;
using NetLah.Common;
using Xunit;

namespace NetLah.Abstractions.Test
{
    public class TimeZoneLocalHelperTest
    {
        [Fact]
        public void GetSingaporeTimeZoneSuccess()
        {
            var tz = TimeZoneLocalHelper.GetSingaporeOrCustomTimeZone();

            Assert.Equal(TimeSpan.FromHours(8), tz.BaseUtcOffset);
            Assert.Contains("+08:00", tz.DisplayName);
        }
    }
}
