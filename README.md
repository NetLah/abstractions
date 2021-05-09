# NetLah.Abstractions - .NET Library

[NetLah.Abstractions](https://www.nuget.org/packages/NetLah.Abstractions/) is a library which contains a set of reusable classes for storage and retrieve .NET assembly build date time at runtime. These libray classes are `ApplicationInfo`, `AssemblyInfo`, `AssemblyBuildDateAttribute`.

## Nuget package

[![NuGet](https://img.shields.io/nuget/v/NetLah.Abstractions.svg?style=flat-square&label=nuget&colorB=00b200)](https://www.nuget.org/packages/NetLah.Abstractions/)

## Build Status

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2FNetLah%2Fabstractions%2Fbadge&style=flat)](https://actions-badge.atrox.dev/NetLah/abstractions/goto)

## Getting started

This solution come from the idea of [Gérald Barré on Meziantou's blog Getting the date of build of a .NET assembly at runtime](https://www.meziantou.net/getting-the-date-of-build-of-a-dotnet-assembly-at-runtime.htm)

### 1. Add the attribute declaration in the .csproj

```
<ItemGroup>
  <AssemblyAttribute Include="NetLah.Runtime.AssemblyBuildDateAttribute">
    <_Parameter1>$([System.DateTime]::UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"))</_Parameter1>
  </AssemblyAttribute>
</ItemGroup>
```

### 2. Retrieve build datetime of assemly

```
var asm = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
Console.WriteLine($"AssemblyTitle:{asm.Title}");
Console.WriteLine($"Version:{asm.InformationalVersion} BuildTime:{asm.BuildTimestampLocal}; Framework:{asm.FrameworkName}");
```

The output:

```
AssemblyTitle:NetLah.Abstractions
Version:0.0.0-alpha.0.1 BuildTime:2021-05-09T12:26:28+08:00; Framework:.NETStandard,Version=v2.1
```

### 3. Or Retrieve build datetime of entry assemly

```
var appInfo = ApplicationInfo.Initialize(null);
Console.WriteLine($"AppTitle:{appInfo.Title}");
Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal};Framework:{appInfo.FrameworkName}");
```

Output:

```
AppTitle:SampleApp
Version:0.0.0-alpha.0.1 BuildTime:2021-05-09T12:26:56+08:00; Framework:.NETCoreApp,Version=v5.0
```

### 4. Use `Directory.Build.props` to declare build date attribute to all projects inside the solution

```
<Project>

  <PropertyGroup>
   ...
  </PropertyGroup>

  <ItemGroup>
    <AssemblyAttribute Include="NetLah.Runtime.AssemblyBuildDateAttribute">
      <_Parameter1>$([System.DateTime]::UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"))</_Parameter1>
    </AssemblyAttribute>
  </ItemGroup>

</Project>
```
