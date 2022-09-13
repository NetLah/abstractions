using NetLah.Runtime;
using System.Reflection;
using Xunit;

namespace NetLah.Abstractions.Test;

public class BuildTimeHelperTest
{
    [Fact]
    public void ParseDateTimeUtcToDateTimeOffsetSuccess()
    {
        var attrs = new[] {
            new AssemblyMetadataAttribute("Key1", "Value2"),
            new AssemblyMetadataAttribute("BuildTime", "2021-05-08T05:25:59")
        };

        var buildTime = BuildTimeHelper.ParseBuildTime(attrs);

        var expected = new DateTimeOffset(2021, 5, 8, 5, 25, 59, TimeSpan.Zero);
        Assert.Equal(expected, buildTime);
        Assert.Equal(expected.Offset, buildTime?.Offset);
    }
}
