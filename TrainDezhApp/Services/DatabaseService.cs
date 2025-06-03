using Microsoft.EntityFrameworkCore;
using TrainDezhApp.Models;
using System.Text.Json;

namespace TrainDezhApp.Services;

public interface IDatabaseService
{
    Task<DatabaseSettings> GetSettingsAsync();
    Task<bool> SaveSettingsAsync(DatabaseSettings settings);
    Task<bool> TestConnectionAsync(DatabaseSettings settings);
    Task<bool> InitializeDatabaseAsync();
    string GetCurrentConnectionString();
}

public class DatabaseService : IDatabaseService
{
    private readonly string _settingsFilePath;
    private DatabaseSettings? _currentSettings;

    public DatabaseService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appDataPath, "TrainDezhApp");
        Directory.CreateDirectory(appFolder);
        _settingsFilePath = Path.Combine(appFolder, "database_settings.json");
    }

    public async Task<DatabaseSettings> GetSettingsAsync()
    {
        if (_currentSettings != null)
            return _currentSettings;

        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = await File.ReadAllTextAsync(_settingsFilePath);
                _currentSettings = JsonSerializer.Deserialize<DatabaseSettings>(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки настроек БД: {ex.Message}");
        }

        _currentSettings ??= new DatabaseSettings
        {
            Id = 1,
            Host = "localhost",
            Port = 5432,
            Database = "train_dezh",
            Username = "postgres",
            Password = "",
            UseSSL = false,
            ConnectionTimeout = 30
        };

        return _currentSettings;
    }

    public async Task<bool> SaveSettingsAsync(DatabaseSettings settings)
    {
        try
        {
            settings.LastUpdated = DateTime.UtcNow;
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            
            await File.WriteAllTextAsync(_settingsFilePath, json);
            _currentSettings = settings;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения настроек БД: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> TestConnectionAsync(DatabaseSettings settings)
    {
        try
        {
            var options = new DbContextOptionsBuilder<TrainDezhDbContext>()
                .UseNpgsql(settings.ConnectionString)
                .Options;

            using var context = new TrainDezhDbContext(options);
            await context.Database.CanConnectAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения к БД: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> InitializeDatabaseAsync()
    {
        try
        {
            var settings = await GetSettingsAsync();
            var options = new DbContextOptionsBuilder<TrainDezhDbContext>()
                .UseNpgsql(settings.ConnectionString)
                .Options;

            using var context = new TrainDezhDbContext(options);
            
            // Создание БД если не существует
            await context.Database.EnsureCreatedAsync();
            
            // Применение миграций
            await context.Database.MigrateAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка инициализации БД: {ex.Message}");
            return false;
        }
    }

    public string GetCurrentConnectionString()
    {
        return _currentSettings?.ConnectionString ?? "Настройки не загружены";
    }
}