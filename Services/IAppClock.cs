using System;

namespace FeatureFramework;

public interface IAppClock
{
    DateTimeOffset StartedUtc { get; }
}
