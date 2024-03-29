﻿using System.Diagnostics;

namespace NetLah.Diagnostics;

/// <summary>
/// Referrence from https://github.com/dotnet/runtime/blob/c9f7f7389/src/libraries/Microsoft.Extensions.Http/src/ValueStopwatch.cs<br/>
/// And from https://www.meziantou.net/how-to-measure-elapsed-time-without-allocating-a-stopwatch.htm
/// </summary>
public readonly struct ValueStopwatch
{
    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

    private readonly long _startTimestamp;

    public readonly bool IsActive => _startTimestamp != 0;

    private ValueStopwatch(long startTimestamp)
    {
        _startTimestamp = startTimestamp;
    }

    public static ValueStopwatch StartNew()
    {
        return new(Stopwatch.GetTimestamp());
    }

    public TimeSpan GetElapsedTime()
    {
        // Start timestamp can't be zero in an initialized ValueStopwatch. It would have to be literally the first thing executed when the machine boots to be 0.
        // So it being 0 is a clear indication of default(ValueStopwatch)
        if (!IsActive)
        {
            throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
        }

        var end = Stopwatch.GetTimestamp();
        var timestampDelta = end - _startTimestamp;
        var ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}
