using System;
using System.Collections.Concurrent;
using System.Windows.Controls;

namespace FeatureFramework;

public sealed class NavigationService : INavigationService
{
    private readonly ConcurrentDictionary<string, Func<UserControl>> _factories = new();

    public void Register(string key, Func<UserControl> factory)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key is required.", nameof(key));
        _factories[key] = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public UserControl Resolve(string key)
    {
        if (!_factories.TryGetValue(key, out var factory))
            throw new InvalidOperationException($"No view registered for key '{key}'.");
        return factory();
    }
}
