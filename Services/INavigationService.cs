using System;
using System.Windows.Controls;

namespace FeatureFramework;

public interface INavigationService
{
    void Register(string key, Func<UserControl> factory);
    UserControl Resolve(string key);
}
