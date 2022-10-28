namespace NetLah.Diagnostics;

public interface IApplicationInfo : IApplicationExecutionTime, IAssemblyInfo
{
    IAssemblyInfo AssemblyInfo { get; }
}
