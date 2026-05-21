using NetLah.Diagnostics;
using System.Reflection;
using Xunit;

namespace NetLah.Abstractions.Test;

public class AssemblyInfoTest
{
    public class AssemblyInfoTestData : TheoryData<Assembly>
    {
        public AssemblyInfoTestData()
        {
            Add(typeof(AssemblyInfo).Assembly);
            Add(typeof(AssemblyInfoTest).Assembly);
        }
    }

    [Theory]
    [ClassData(typeof(AssemblyInfoTestData))]
    public void AssemblyBuildDate_Exist(Assembly assembly)
    {
        var assemblyInfo = new AssemblyInfo(assembly);

        var buildDate = assemblyInfo.BuildDate;

        Assert.NotNull(buildDate);
    }

    [Theory]
    [ClassData(typeof(AssemblyInfoTestData))]
    public void AssemblyBuildDate_MustHave(Assembly assembly)
    {
        var attrs0 = assembly.GetCustomAttributes<AssemblyMetadataAttribute>().ToArray();
        var attrs = attrs0.Where(a => a.Key != "BuildTime").ToArray();

        var buildTime = NetLah.Runtime.BuildDateHelper.ParseBuildDate(attrs);

        Assert.NotNull(buildTime);
        Assert.DoesNotContain(attrs, a => a.Key == "BuildTime");
        Assert.Contains(attrs, a => a.Key == "BuildDate");
    }

    [Theory]
    [ClassData(typeof(AssemblyInfoTestData))]
    public void AssemblyBuildTime_Compatible(Assembly assembly)
    {
        var attrs0 = assembly.GetCustomAttributes<AssemblyMetadataAttribute>().ToArray();
        var attrs = attrs0.Where(a => a.Key != "BuildDate").ToArray();

        var buildTime = NetLah.Runtime.BuildDateHelper.ParseBuildDate(attrs);

        Assert.NotNull(buildTime);
        Assert.DoesNotContain(attrs, a => a.Key == "BuildDate");
        Assert.Contains(attrs, a => a.Key == "BuildTime");
    }

    [Fact]
    public void ApplicationBuildDate_Exist()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(BuildDateHelperTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);

        var buildDate = applicationInfo.BuildDate;

        Assert.NotNull(buildDate);
    }

    [Fact]
    public async Task Application_Properties()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(BuildDateHelperTest).Assembly);

        Assert.NotNull(applicationInfo);
        Assert.NotNull(applicationInfo.AssemblyInfo);
        await Task.Delay(200);
        Assert.NotEqual(TimeSpan.Zero, applicationInfo.Uptime);
    }

    [Theory]
    [ClassData(typeof(AssemblyInfoTestData))]
    [Obsolete("Use BuildDate property")]
    public void AssemblyBuildTime_Exist(Assembly assembly)
    {
        var assemblyInfo = new AssemblyInfo(assembly);

        var buildTime = assemblyInfo.BuildTime;

        Assert.NotNull(buildTime);
    }

    [Fact]
    [Obsolete("Use BuildDate property")]
    public void ApplicationBuildTime_Exist()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

        Assert.NotNull(ApplicationInfoReference.Instance);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);

        var buildTime = applicationInfo.BuildTime;

        Assert.NotNull(buildTime);
    }

    [Fact]
    public void InitializeApplicationInfo_Exception()
    {
        ApplicationInfoReference.SetAny();

        var ex = Assert.Throws<InvalidOperationException>(() => ApplicationInfo.Initialize(typeof(BuildDateHelperTest).Assembly));

        Assert.StartsWith("ApplicationInfo is already initialized with assembly: ", ex.Message);
    }

    [Fact]
    public void TryInitializeApplicationInfoNull()
    {
        ApplicationInfoReference.Reset();

        var applicationInfo = ApplicationInfo.TryInitialize(typeof(BuildDateHelperTest).Assembly);

        Assert.NotNull(applicationInfo);
        Assert.NotSame(EmptyApplicationInfo.Default, applicationInfo);
    }

    [Fact]
    public void TryInitializeApplicationInfoNotNull()
    {
        ApplicationInfoReference.SetAny();

        var applicationInfo = ApplicationInfo.TryInitialize(typeof(BuildDateHelperTest).Assembly);

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
