using System.Collections.Generic;
using System.Reflection;
using NetLah.Diagnostics;
using Xunit;

namespace NetLah.Abstractions.Test
{
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
        public void AssemblyBuidDate_Exist(Assembly assembly)
        {
            var assemblyInfo = new AssemblyInfo(assembly);

            var buildDate = assemblyInfo.BuildDate;

            Assert.NotNull(buildDate);
        }

        [Fact]
        public void ApplicationBuidDate_Exist()
        {
            var applicationInfo = ApplicationInfo.Initialize(typeof(AssemblyBuildDateAttributeTest).Assembly);

            var buildDate = applicationInfo.BuildDate;

            Assert.NotNull(buildDate);
        }
    }
}
