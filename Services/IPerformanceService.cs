using System;

namespace FeatureFramework;

public interface IPerformanceService
{
    PerfOperation Start(string operationName);
}

public sealed class PerfOperation : IDisposable
{
    private readonly Action<PerfOperation> _onDispose;
    private readonly long _startedTicks;
    private bool _success;

    internal PerfOperation(string name, Action<PerfOperation> onDispose)
    {
        Name = name;
        _onDispose = onDispose;
        _startedTicks = DateTime.UtcNow.Ticks;
    }

    public string Name { get; }
    public long ElapsedMilliseconds => (DateTime.UtcNow.Ticks - _startedTicks) / TimeSpan.TicksPerMillisecond;

    public void MarkSuccess() => _success = true;
    public bool Success => _success;

    public void Dispose() => _onDispose(this);
}
