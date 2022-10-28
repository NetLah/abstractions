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
            new object[] { typeof(AssemblyInfoTest).Assembly },
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
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);

        var buildTime = applicationInfo.BuildTime;

        Assert.NotNull(buildTime);
    }

    [Fact]
    public async Task Application_Properties()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(applicationInfo);
        Assert.NotNull(applicationInfo.AssemblyInfo);
        await Task.Delay(200);
        Assert.NotEqual(TimeSpan.Zero, applicationInfo.Uptime);
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
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);

        var buildDate = applicationInfo.BuildDate;

        Assert.NotNull(buildDate);
    }

    [Fact]
    public void InitializeApplicationInfo_Exception()
    {
        ApplicationInfoReference.SetAny();

        var ex = Assert.Throws<InvalidOperationException>(() => ApplicationInfo.Initialize(typeof(BuildTimeHelperTest).Assembly));

        Assert.StartsWith("ApplicationInfo is already initialized with assembly: ", ex.Message);
    }

    [Fact]
    public void TryInitializeApplicationInfoNull()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.TryInitialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);
    }

    [Fact]
    public void TryInitializeApplicationInfoNotNull()
    {
        ApplicationInfoReference.SetAny();

        var applicationInfo = ApplicationInfo.TryInitialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);
    }

    [Fact]
    public void ApplicationInfoInstanceOrDefault()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.InstanceOrDefault;
        var applicationInfo1 = ApplicationInfo.InstanceOrDefault;

        Assert.NotNull(applicationInfo);
        Assert.Same(applicationInfo, applicationInfo1);
        Assert.Same(EmptyApplicationInfo.Default, applicationInfo);
    }

    [Fact]
    public void ApplicationInfoInstance_TryInitializing()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Instance;
        var applicationInfo1 = ApplicationInfo.Instance;

        Assert.NotNull(applicationInfo);
        Assert.Same(applicationInfo, applicationInfo1);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);
    }
}
