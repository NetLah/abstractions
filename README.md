# NetLah.Abstractions - .NET Library

[NetLah.Abstractions](https://www.nuget.org/packages/NetLah.Abstractions/) is a library which contains a set of reusable classes for storage and retrieve .NET assembly build date time at runtime. These libray classes are `ApplicationInfo`, `AssemblyInfo`, `AssemblyBuildDateAttribute`.

## Nuget package

[![NuGet](https://img.shields.io/nuget/v/NetLah.Abstractions.svg?style=flat-square&label=nuget&colorB=00b200)](https://www.nuget.org/packages/NetLah.Abstractions/)

## Build Status

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2FNetLah%2Fabstractions%2Fbadge%3Fref%3Dmain&style=flat)](https://actions-badge.atrox.dev/NetLah/abstractions/goto?ref=main)

## Getting started

This solution come from the idea of [Gérald Barré on Meziantou's blog Getting the date of build of a .NET assembly at runtime](https://www.meziantou.net/getting-the-date-of-build-of-a-dotnet-assembly-at-runtime.htm)

### 1. Add/Update PackageReference with PrivateAssets=build in .csproj

```xml
<ItemGroup>
  <PackageReference Include="NetLah.Abstractions" Version="0.1.1" PrivateAssets="build"/>
</ItemGroup>
```

### 2. Output during build

```txt
Microsoft (R) Build Engine version 16.10.1+2fd48ab73 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  SampleApp -> C:\Work\NetLah\abstractions\samples\SampleApp\bin\Debug\net5.0\SampleApp.dll
  [set] AssemblyBuildDate: 2021-06-10T05:28:33

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.84
```

### 2. Retrieve build datetime of assemly

```csharp
var asm = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
Console.WriteLine($"AssemblyTitle:{asm.Title}");
Console.WriteLine($"Version:{asm.InformationalVersion} BuildTime:{asm.BuildTimestampLocal}; Framework:{asm.FrameworkName}");
```

The output:

```text
AssemblyTitle:NetLah.Abstractions
Version:0.0.0-alpha.0.1 BuildTime:2021-05-09T12:26:28+08:00; Framework:.NETStandard,Version=v2.1
```

### 3. Or Retrieve build datetime of entry assemly

```csharp
var appInfo = ApplicationInfo.Initialize(null);
Console.WriteLine($"AppTitle:{appInfo.Title}");
Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal};Framework:{appInfo.FrameworkName}");
```

Output:

```text
AppTitle:SampleApp
Version:0.0.0-alpha.0.1 BuildTime:2021-05-09T12:26:56+08:00; Framework:.NETCoreApp,Version=v5.0
```

### 4. Use `Directory.Build.targets` to declare build date attribute to all projects inside the solution

```xml
<ItemGroup>
  <PackageReference Update="NetLah.Abstractions" Version="0.1.1" PrivateAssets="build" />
</ItemGroup>
```
