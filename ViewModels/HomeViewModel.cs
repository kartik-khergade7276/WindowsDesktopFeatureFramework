using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureFramework;

public sealed class HomeViewModel : ViewModelBase
{
    private readonly IPerformanceService _perf;
    private readonly IAppLogger _logger;

    public HomeViewModel(IPerformanceService perf, IAppLogger logger)
    {
        _perf = perf;
        _logger = logger;

        SimulateWorkCommand = new RelayCommand(async () => await SimulateWorkAsync());
        LogInfoCommand = new RelayCommand(() =>
        {
            var msg = "User-triggered info event captured from Home.";
            _logger.Info(msg);
            AddUiLog("INFO", msg);
            LastActionSummary = "Wrote an info log entry to the UI feed and log file.";
        });
        ClearLogsCommand = new RelayCommand(() =>
        {
            RecentLogs.Clear();
            LastActionSummary = "Cleared UI log feed.";
        });

        AddUiLog("INFO", "App started. Ready.");
    }

    public ObservableCollection<string> RecentLogs { get; } = new();

    public RelayCommand SimulateWorkCommand { get; }
    public RelayCommand LogInfoCommand { get; }
    public RelayCommand ClearLogsCommand { get; }

    private string _lastActionSummary = "Tip: click “Simulate Feature Work” to see timing + observability.";
    public string LastActionSummary
    {
        get => _lastActionSummary;
        set => Set(ref _lastActionSummary, value);
    }

    private async Task SimulateWorkAsync()
    {
        var opName = "SimulatedFeatureWork";
        using var op = _perf.Start(opName);

        AddUiLog("INFO", $"Starting {opName}…");
        _logger.Info($"Starting {opName}");

        // Simulate work that might be “complex and ambiguous”: IO + CPU
        await Task.Delay(220);
        var sum = 0L;
        for (int i = 0; i < 650_000; i++)
            sum += i % 7;

        await Task.Delay(180);

        op.MarkSuccess();

        var elapsed = op.ElapsedMilliseconds;
        var msg = $"{opName} completed in {elapsed} ms (checksum {sum}).";

        _logger.Info(msg);
        AddUiLog("OK", msg);
        LastActionSummary = "Simulated a feature path and captured timing + a persisted log entry.";
    }

    private void AddUiLog(string level, string message)
    {
        var line = $"{DateTime.Now:HH:mm:ss} [{level}] {message}";
        RecentLogs.Insert(0, line);

        // Keep UI list tidy
        while (RecentLogs.Count > 80)
            RecentLogs.RemoveAt(RecentLogs.Count - 1);
    }
}
