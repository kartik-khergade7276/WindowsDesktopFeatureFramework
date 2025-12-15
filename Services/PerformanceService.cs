using System;

namespace FeatureFramework;

public sealed class PerformanceService : IPerformanceService
{
    private readonly IAppLogger _logger;

    public PerformanceService(IAppLogger logger)
    {
        _logger = logger;
    }

    public PerfOperation Start(string operationName)
    {
        return new PerfOperation(operationName, op =>
        {
            var status = op.Success ? "SUCCESS" : "DONE";
            _logger.Info($"Perf: {op.Name} {status} in {op.ElapsedMilliseconds} ms");
        });
    }
}
