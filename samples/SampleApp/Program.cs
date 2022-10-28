using NetLah.Diagnostics;

var appInfo = ApplicationInfo.Initialize(null);
Console.WriteLine($"AppTitle: {appInfo.Title}");
Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal}; Framework:{appInfo.FrameworkName}");
Console.WriteLine($"StartTime:{appInfo.StartTime} Uptime:{appInfo.Uptime}");

Console.WriteLine("");

var asm = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
Console.WriteLine($"AssemblyTitle: {asm.Title}");
Console.WriteLine($"Version:{asm.InformationalVersion} BuildTime:{asm.BuildTimestampLocal}; Framework:{asm.FrameworkName}");
