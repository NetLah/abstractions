using NetLah.Diagnostics;
using System.Reflection;
using Xunit;

namespace NetLah.Abstractions.Test;

public class AssemblyInfoTest
{
    public static IEnumerable<object[]> AssemblyData =>
        new List<object[]>
        {
            new object[] { typeof(AssemblyInfo).Assembly},
            new object[] { typeof(AssemblyBuildDateAttributeTest).Assembly },
        };

    [Theory]
    [MemberData(nameof(AssemblyData))]
    public void AssemblyBuildTime_Exist(Assembly assembly)
    {
        var assemblyInfo = new AssemblyInfo(assembly);

        var buildDate = assemblyInfo.BuildTime;

        Assert.NotNull(buildDate);
    }

    [Fact]
    public void ApplicationBuildTime_Exist()
    {
        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        var buildDate = applicationInfo.BuildTime;

        Assert.NotNull(buildDate);
    }

    [Theory]
    [MemberData(nameof(AssemblyData))]
    [Obsolete("Use BuildTime property")]
    public void AssemblyBuildDate_Exist(Assembly assembly)
    {
        var assemblyInfo = new AssemblyInfo(assembly);

        var buildDate = assemblyInfo.BuildDate;

        Assert.NotNull(buildDate);
    }

    [Fact]
    [Obsolete("Use BuildTime property")]
    public void ApplicationBuildDate_Exist()
    {
        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        var buildDate = applicationInfo.BuildDate;

        Assert.NotNull(buildDate);
    }
}
