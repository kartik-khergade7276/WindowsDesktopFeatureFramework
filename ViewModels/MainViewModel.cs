using System;
using System.Windows.Controls;
using FeatureFramework.Views;

namespace FeatureFramework;

public sealed class MainViewModel : ViewModelBase
{
    private readonly INavigationService _nav;
    private readonly IAppClock _clock;

    private UserControl? _currentView;

    public MainViewModel(INavigationService nav, IAppClock clock, HomeViewModel homeVm, SettingsViewModel settingsVm)
    {
        _nav = nav;
        _clock = clock;

        // Register pages once; view creation is cheap here.
        _nav.Register("home", () => new HomeView { DataContext = homeVm });
        _nav.Register("settings", () => new SettingsView { DataContext = settingsVm });

        GoHomeCommand = new RelayCommand(() => Navigate("home"));
        GoSettingsCommand = new RelayCommand(() => Navigate("settings"));

        Navigate("home");
    }

    public RelayCommand GoHomeCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public UserControl? CurrentView
    {
        get => _currentView;
        private set => Set(ref _currentView, value);
    }

    public string HealthSummary =>
        $"Uptime: {(DateTimeOffset.UtcNow - _clock.StartedUtc):hh\:mm\:ss} • Framework: WPF/MVVM • Runtime: .NET";

    private void Navigate(string key)
    {
        CurrentView = _nav.Resolve(key);
        RaisePropertyChanged(nameof(HealthSummary));
    }
}
