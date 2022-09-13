using NetLah.Runtime;
using Xunit;

namespace NetLah.Abstractions.Test;

public class AssemblyBuildTimeAttributeTest
{
    [Fact]
    public void ParseDateTimeUtcToDateTimeOffsetSuccess()
    {
        var attr = new AssemblyBuildTimeAttribute("2021-05-08T05:25:59");

        var expected = new DateTimeOffset(2021, 5, 8, 5, 25, 59, TimeSpan.Zero);
        Assert.Equal(expected, attr.DateTime);
        Assert.Equal(expected.Offset, attr.DateTime?.Offset);
    }
}
