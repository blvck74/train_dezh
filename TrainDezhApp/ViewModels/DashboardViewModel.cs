using System.Collections.ObjectModel;
using ReactiveUI;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    public DashboardViewModel()
    {
        var databaseService = new DatabaseService();
        _dataService = new DataService(databaseService);
        
        StatCards = new ObservableCollection<StatCardViewModel>
        {
            new StatCardViewModel("Активные работы", "0", "🔧", "#2563EB"),
            new StatCardViewModel("Аварии сегодня", "0", "⚠️", "#EF4444"),
            new StatCardViewModel("Завершено работ", "0", "✅", "#10B981"),
            new StatCardViewModel("Персонал на смене", "0", "👥", "#8B5CF6")
        };

        RecentActivities = new ObservableCollection<ActivityViewModel>();
        
        LoadDashboardData();
    }

    public ObservableCollection<StatCardViewModel> StatCards { get; }
    public ObservableCollection<ActivityViewModel> RecentActivities { get; }

    private async void LoadDashboardData()
    {
        try
        {
            // Загружаем статистику
            var activeWorks = await _dataService.GetActiveWorksCountAsync();
            var criticalAccidents = await _dataService.GetCriticalAccidentsCountAsync();
            var staffOnShift = await _dataService.GetStaffOnShiftCountAsync();
            var equipmentReadiness = await _dataService.GetEquipmentReadinessAsync();

            // Обновляем карточки
            StatCards[0].UpdateValue(activeWorks.ToString());
            StatCards[1].UpdateValue(criticalAccidents.ToString());
            StatCards[2].UpdateValue($"{equipmentReadiness}%");
            StatCards[3].UpdateValue(staffOnShift.ToString());

            // Загружаем последние события
            await LoadRecentActivities();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки данных дашборда: {ex.Message}");
            // Показываем тестовые данные при ошибке
            LoadTestData();
        }
    }

    private async Task LoadRecentActivities()
    {
        try
        {
            RecentActivities.Clear();
            
            // Получаем последние работы
            var works = await _dataService.GetWorksAsync();
            foreach (var work in works.Take(2))
            {
                RecentActivities.Add(new ActivityViewModel(
                    $"Работа: {work.Title}",
                    work.CreatedAt.ToString("HH:mm"),
                    "🔧"
                ));
            }

            // Получаем последние аварии
            var accidents = await _dataService.GetAccidentsAsync();
            foreach (var accident in accidents.Take(2))
            {
                RecentActivities.Add(new ActivityViewModel(
                    $"Авария: {accident.Description}",
                    accident.OccurredAt.ToString("HH:mm"),
                    "⚠️"
                ));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки последних событий: {ex.Message}");
        }
    }

    private void LoadTestData()
    {
        StatCards[0].UpdateValue("12");
        StatCards[1].UpdateValue("3");
        StatCards[2].UpdateValue("95%");
        StatCards[3].UpdateValue("8");

        RecentActivities.Clear();
        RecentActivities.Add(new ActivityViewModel("Начата работа по замене насоса №3", "10:30", "🔧"));
        RecentActivities.Add(new ActivityViewModel("Зафиксирована авария на линии А", "09:45", "⚠️"));
        RecentActivities.Add(new ActivityViewModel("Завершена проверка оборудования", "09:15", "✅"));
        RecentActivities.Add(new ActivityViewModel("Передача смены от бригады №2", "08:00", "🔄"));
    }

    public async Task RefreshData()
    {
        await Task.Run(LoadDashboardData);
    }
}

public class StatCardViewModel : ReactiveObject
{
    private string _value;

    public StatCardViewModel(string title, string value, string icon, string color)
    {
        Title = title;
        _value = value;
        Icon = icon;
        Color = color;
    }

    public string Title { get; }
    public string Value
    {
        get => _value;
        private set => this.RaiseAndSetIfChanged(ref _value, value);
    }
    public string Icon { get; }
    public string Color { get; }

    public void UpdateValue(string newValue)
    {
        Value = newValue;
    }
}

public class ActivityViewModel
{
    public ActivityViewModel(string description, string time, string icon)
    {
        Description = description;
        Time = time;
        Icon = icon;
    }

    public string Description { get; }
    public string Time { get; }
    public string Icon { get; }
}