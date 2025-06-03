using ReactiveUI;

namespace TrainDezhApp.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private string _databaseConnection = "Server=localhost;Database=TrainDezh;User Id=postgres;Password=;";
    private bool _autoSave = true;
    private int _refreshInterval = 30;

    public string DatabaseConnection
    {
        get => _databaseConnection;
        set => this.RaiseAndSetIfChanged(ref _databaseConnection, value);
    }

    public bool AutoSave
    {
        get => _autoSave;
        set => this.RaiseAndSetIfChanged(ref _autoSave, value);
    }

    public int RefreshInterval
    {
        get => _refreshInterval;
        set => this.RaiseAndSetIfChanged(ref _refreshInterval, value);
    }
}