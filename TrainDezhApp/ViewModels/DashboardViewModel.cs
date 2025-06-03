using System.Collections.ObjectModel;

namespace TrainDezhApp.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    public DashboardViewModel()
    {
        StatCards = new ObservableCollection<StatCardViewModel>
        {
            new StatCardViewModel("Активные работы", "12", "🔧", "#2563EB"),
            new StatCardViewModel("Аварии сегодня", "3", "⚠️", "#EF4444"),
            new StatCardViewModel("Завершено работ", "28", "✅", "#10B981"),
            new StatCardViewModel("Персонал на смене", "45", "👥", "#8B5CF6")
        };

        RecentActivities = new ObservableCollection<ActivityViewModel>
        {
            new ActivityViewModel("Начата работа по замене насоса №3", "10:30", "🔧"),
            new ActivityViewModel("Зафиксирована авария на линии А", "09:45", "⚠️"),
            new ActivityViewModel("Завершена проверка оборудования", "09:15", "✅"),
            new ActivityViewModel("Передача смены от бригады №2", "08:00", "🔄")
        };
    }

    public ObservableCollection<StatCardViewModel> StatCards { get; }
    public ObservableCollection<ActivityViewModel> RecentActivities { get; }
}

public class StatCardViewModel
{
    public StatCardViewModel(string title, string value, string icon, string color)
    {
        Title = title;
        Value = value;
        Icon = icon;
        Color = color;
    }

    public string Title { get; }
    public string Value { get; }
    public string Icon { get; }
    public string Color { get; }
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