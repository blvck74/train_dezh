using ReactiveUI;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IDatabaseService _databaseService;
    private DatabaseSettings _databaseSettings;
    private bool _isEditingDatabase;
    private string _connectionStatus = "Не проверено";
    private bool _isTestingConnection;
    private bool _autoSave = true;
    private int _refreshInterval = 30;

    public SettingsViewModel()
    {
        _databaseService = new DatabaseService();
        _databaseSettings = new DatabaseSettings();
        
        EditDatabaseCommand = ReactiveCommand.CreateFromTask(EditDatabase);
        SaveDatabaseCommand = ReactiveCommand.CreateFromTask(SaveDatabase);
        CancelEditCommand = ReactiveCommand.Create(CancelEdit);
        TestConnectionCommand = ReactiveCommand.CreateFromTask(TestConnection);
        InitializeDatabaseCommand = ReactiveCommand.CreateFromTask(InitializeDatabase);
        
        LoadSettings();
    }

    public string Title => "Параметры";

    public DatabaseSettings DatabaseSettings
    {
        get => _databaseSettings;
        set => this.RaiseAndSetIfChanged(ref _databaseSettings, value);
    }

    public bool IsEditingDatabase
    {
        get => _isEditingDatabase;
        set => this.RaiseAndSetIfChanged(ref _isEditingDatabase, value);
    }

    public string ConnectionStatus
    {
        get => _connectionStatus;
        set => this.RaiseAndSetIfChanged(ref _connectionStatus, value);
    }

    public bool IsTestingConnection
    {
        get => _isTestingConnection;
        set => this.RaiseAndSetIfChanged(ref _isTestingConnection, value);
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

    public ReactiveCommand<Unit, Unit> EditDatabaseCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveDatabaseCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelEditCommand { get; }
    public ReactiveCommand<Unit, Unit> TestConnectionCommand { get; }
    public ReactiveCommand<Unit, Unit> InitializeDatabaseCommand { get; }

    private async void LoadSettings()
    {
        try
        {
            DatabaseSettings = await _databaseService.GetSettingsAsync();
            ConnectionStatus = "Настройки загружены";
        }
        catch (Exception ex)
        {
            ConnectionStatus = $"Ошибка загрузки: {ex.Message}";
        }
    }

    private Task EditDatabase()
    {
        IsEditingDatabase = true;
        // Создаем копию для редактирования
        DatabaseSettings = new DatabaseSettings
        {
            Id = DatabaseSettings.Id,
            Host = DatabaseSettings.Host,
            Port = DatabaseSettings.Port,
            Database = DatabaseSettings.Database,
            Username = DatabaseSettings.Username,
            Password = DatabaseSettings.Password,
            UseSSL = DatabaseSettings.UseSSL,
            ConnectionTimeout = DatabaseSettings.ConnectionTimeout
        };
        return Task.CompletedTask;
    }

    private async Task SaveDatabase()
    {
        try
        {
            var success = await _databaseService.SaveSettingsAsync(DatabaseSettings);
            if (success)
            {
                ConnectionStatus = "Настройки сохранены";
                IsEditingDatabase = false;
            }
            else
            {
                ConnectionStatus = "Ошибка сохранения настроек";
            }
        }
        catch (Exception ex)
        {
            ConnectionStatus = $"Ошибка: {ex.Message}";
        }
    }

    private void CancelEdit()
    {
        IsEditingDatabase = false;
        LoadSettings(); // Перезагружаем исходные настройки
    }

    private async Task TestConnection()
    {
        IsTestingConnection = true;
        ConnectionStatus = "Проверка подключения...";
        
        try
        {
            var success = await _databaseService.TestConnectionAsync(DatabaseSettings);
            ConnectionStatus = success ? "✅ Подключение успешно" : "❌ Ошибка подключения";
        }
        catch (Exception ex)
        {
            ConnectionStatus = $"❌ Ошибка: {ex.Message}";
        }
        finally
        {
            IsTestingConnection = false;
        }
    }

    private async Task InitializeDatabase()
    {
        try
        {
            ConnectionStatus = "Инициализация базы данных...";
            var success = await _databaseService.InitializeDatabaseAsync();
            ConnectionStatus = success ? "✅ База данных инициализирована" : "❌ Ошибка инициализации";
        }
        catch (Exception ex)
        {
            ConnectionStatus = $"❌ Ошибка: {ex.Message}";
        }
    }
}