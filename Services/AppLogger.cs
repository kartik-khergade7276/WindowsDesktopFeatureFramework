using System;
using System.IO;

namespace FeatureFramework;

public sealed class AppLogger : IAppLogger
{
    private readonly object _lock = new();
    private bool _verbose;

    public AppLogger()
    {
        var root = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FeatureFramework",
            "logs");

        Directory.CreateDirectory(root);
        LogFilePath = Path.Combine(root, $"app-{DateTime.Now:yyyyMMdd}.log");

        Info("Logger initialized.");
        Info($"Log file: {LogFilePath}");
    }

    public string LogFilePath { get; }

    public void SetVerbose(bool enabled)
    {
        _verbose = enabled;
        Info($"Verbose logging {(enabled ? "enabled" : "disabled")}.");
    }

    public void Info(string message) => Write("INFO", message);
    public void Warn(string message) => Write("WARN", message);
    public void Error(string message) => Write("ERROR", message);

    private void Write(string level, string message)
    {
        if (!_verbose && level == "INFO" && message.StartsWith("Perf:", StringComparison.Ordinal))
        {
            // In non-verbose mode we still write perf logs, but keep them minimal.
        }

        var line = $"{DateTime.Now:O} [{level}] {message}";
        lock (_lock)
        {
            File.AppendAllText(LogFilePath, line + Environment.NewLine);
        }
    }
}
