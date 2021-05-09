using System;
using NetLah.Diagnostics;

namespace SampleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var appInfo = ApplicationInfo.Initialize(null);
            Console.WriteLine($"AppTitle:{appInfo.Title}");
            Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal}; Framework:{appInfo.FrameworkName}");

            Console.WriteLine("");

            var asm = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
            Console.WriteLine($"AssemblyTitle:{asm.Title}");
            Console.WriteLine($"Version:{asm.InformationalVersion} BuildTime:{asm.BuildTimestampLocal}; Framework:{asm.FrameworkName}");
        }
    }
}
