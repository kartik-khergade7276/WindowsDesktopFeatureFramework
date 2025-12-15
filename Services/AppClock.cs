using System;

namespace FeatureFramework;

public sealed class AppClock : IAppClock
{
    public DateTimeOffset StartedUtc { get; } = DateTimeOffset.UtcNow;
}
