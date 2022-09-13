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
            new object[] { typeof(AssemblyBuildTimeAttributeTest).Assembly },
        };

    [Theory]
    [MemberData(nameof(AssemblyData))]
    public void AssemblyBuildTime_Exist(Assembly assembly)
    {
        var assemblyInfo = new AssemblyInfo(assembly);

        var buildTime = assemblyInfo.BuildTime;

        Assert.NotNull(buildTime);
    }

    [Fact]
    public void ApplicationBuildTime_Exist()
    {
        ApplicationInfoReference.Instance = null;

        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildTimeAttributeTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        var buildTime = applicationInfo.BuildTime;

        Assert.NotNull(buildTime);
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
        ApplicationInfoReference.Instance = null;

        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        var buildDate = applicationInfo.BuildDate;

        Assert.NotNull(buildDate);
    }


    [Fact]
    public void ApplicationBuildTime_Exception()
    {
        if (ApplicationInfoReference.Instance == null)
        {
            ApplicationInfo.Initialize(typeof(AssemblyBuildTimeAttributeTest).Assembly);
        }

        var ex = Assert.Throws<InvalidOperationException>(() => ApplicationInfo.Initialize(typeof(AssemblyBuildTimeAttributeTest).Assembly));

        Assert.StartsWith("ApplicationInfo is already initialized with assembly: ", ex.Message);
    }
}
