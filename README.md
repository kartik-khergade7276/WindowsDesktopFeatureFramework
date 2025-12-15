# Windows Desktop Feature Framework (WPF + MVVM + DI)

 project demonstrates:
- WPF UI with a clean layout
- MVVM structure (ViewModels, Commands)
- Simple navigation (Home/Settings)
- Observability: UI log feed + persisted log file
- Lightweight performance timing service

## Prerequisites
- Windows 10/11
- .NET 8 SDK installed

## Run
From a terminal in the project root:

```bash
cd FeatureFramework
dotnet restore
dotnet run
```

Or open `WindowsDesktopFeatureFramework.sln` in Visual Studio 2022+ and press **F5**.

## Where logs go
Logs are written to:
`%LOCALAPPDATA%\FeatureFramework\logs\app-YYYYMMDD.log`

## Next steps you can add quickly
- Real dependency graph (feature -> services -> external deps)
- Structured telemetry (EventSource / ETW)
- Unit tests for ViewModels
- WinUI 3 version of the same framework
