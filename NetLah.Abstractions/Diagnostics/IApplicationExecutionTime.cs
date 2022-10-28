namespace NetLah.Diagnostics;

public interface IApplicationExecutionTime
{
    DateTimeOffset StartTime { get; }
    TimeSpan Uptime { get; }
}
