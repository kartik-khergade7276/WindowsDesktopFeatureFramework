namespace FeatureFramework;

public sealed class SettingsViewModel : ViewModelBase
{
    private readonly IAppLogger _logger;

    public SettingsViewModel(IAppLogger logger)
    {
        _logger = logger;
        LogFilePath = _logger.LogFilePath;
    }

    private bool _verboseLoggingEnabled;
    public bool VerboseLoggingEnabled
    {
        get => _verboseLoggingEnabled;
        set
        {
            if (Set(ref _verboseLoggingEnabled, value))
                _logger.SetVerbose(value);
        }
    }

    public string LogFilePath { get; }
}
