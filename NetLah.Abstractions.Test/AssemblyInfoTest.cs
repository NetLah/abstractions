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

        var assemblyInfo = ApplicationInfo.Initialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(assemblyInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, assemblyInfo);

        var buildTime = assemblyInfo.BuildTime;

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
        ApplicationInfoReference.Reset();

        var assemblyInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(assemblyInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, assemblyInfo);

        var buildDate = assemblyInfo.BuildDate;

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

        var assemblyInfo = ApplicationInfo.TryInitialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(assemblyInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, assemblyInfo);
    }

    [Fact]
    public void TryInitializeApplicationInfoNotNull()
    {
        ApplicationInfoReference.SetAny();

        var assemblyInfo = ApplicationInfo.TryInitialize(typeof(BuildTimeHelperTest).Assembly);

        Assert.NotNull(assemblyInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, assemblyInfo);
    }

    [Fact]
    public void ApplicationInfoInstanceOrDefault()
    {
        ApplicationInfoReference.Reset();

        var assemblyInfo = ApplicationInfo.InstanceOrDefault;
        var assemblyInfo1 = ApplicationInfo.InstanceOrDefault;

        Assert.NotNull(assemblyInfo);
        Assert.Same(assemblyInfo, assemblyInfo1);
        Assert.Same(EmptyApplicationInfo.Default, assemblyInfo);
    }

    [Fact]
    public void ApplicationInfoInstance_TryInitializing()
    {
        ApplicationInfoReference.Reset();

        var assemblyInfo = ApplicationInfo.Instance;
        var assemblyInfo1 = ApplicationInfo.Instance;

        Assert.NotNull(assemblyInfo);
        Assert.Same(assemblyInfo, assemblyInfo1);
        Assert.NotSame(EmptyApplicationInfo.Default, assemblyInfo);
    }
}
