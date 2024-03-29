# NetLah.AssemblyInfo.BuildTime.Target - MSBuild targets

MSBuild target for assembly build-time generation.

## Nuget package

[![NuGet](https://img.shields.io/nuget/v/NetLah.AssemblyInfo.BuildTime.Target.svg?style=flat-square&label=nuget&colorB=00b200)](https://www.nuget.org/packages/NetLah.AssemblyInfo.BuildTime.Target/)

## Build Status

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2FNetLah%2Fabstractions%2Fbadge%3Fref%3Dmain&style=flat)](https://actions-badge.atrox.dev/NetLah/abstractions/goto?ref=main)

## Getting started

### 1. Add/Update PackageReference to .csproj

```xml
<ItemGroup>
  <PackageReference Include="NetLah.Abstractions" Version="0.2.0" />
  <PackageReference Include="NetLah.AssemblyInfo.BuildTime.Target" Version="0.2.0" PrivateAssets="All" />
</ItemGroup>
```

### 2. Output during build

```txt
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  SampleApp -> C:\Work\NetLah\abstractions\samples\SampleApp\bin\Debug\net6.0\SampleApp.dll
  SampleApp [buildTime] := 2021-11-08T21:26:28 (net6.0)

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.84
```

### 2. Retrieve build datetime of assembly

```csharp
var asm = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
Console.WriteLine($"AssemblyTitle:{asm.Title}");
Console.WriteLine($"Version:{asm.InformationalVersion} BuildTime:{asm.BuildTimestampLocal}; Framework:{asm.FrameworkName}");
```

The output:

```text
AssemblyTitle:NetLah.Abstractions
Version:0.2.0-rc2.2 BuildTime:2021-11-08T21:26:28+08:00; Framework:.NETStandard,Version=v2.0
```

### 3. Or Retrieve build datetime of entry assembly

```csharp
var appInfo = ApplicationInfo.Initialize(null);
Console.WriteLine($"AppTitle:{appInfo.Title}");
Console.WriteLine($"Version:{appInfo.InformationalVersion} BuildTime:{appInfo.BuildTimestampLocal};Framework:{appInfo.FrameworkName}");
```

Output:

```text
AppTitle:SampleApp
Version:0.2.0-rc2.2 BuildTime:2021-11-08T21:26:56+08:00; Framework:.NETCoreApp,Version=v6.0
```

### 4. Use `Directory.Build.targets` to declare build date time attribute to all projects inside the solution

- Set build date time to projects that already include packageReference `NetLah.AssemblyInfo.BuildTime.Target`

```xml
<ItemGroup>
  <PackageReference Update="NetLah.Abstractions" Version="0.2.0" />
  <PackageReference Update="NetLah.AssemblyInfo.BuildTime.Target" Version="0.2.0" PrivateAssets="All" />
</ItemGroup>
```

- Set build date time to all projects

```xml
<ItemGroup>
  <PackageReference Update="NetLah.Abstractions" Version="0.2.0" />
  <PackageReference Include="NetLah.AssemblyInfo.BuildTime.Target" Version="0.2.0" PrivateAssets="All" />
</ItemGroup>
```
