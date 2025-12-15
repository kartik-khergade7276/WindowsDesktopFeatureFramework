namespace FeatureFramework;

public interface IAppLogger
{
    string LogFilePath { get; }
    void Info(string message);
    void Warn(string message);
    void Error(string message);
    void SetVerbose(bool enabled);
}
