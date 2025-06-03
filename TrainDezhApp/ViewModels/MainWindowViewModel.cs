using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace TrainDezhApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentPage = null!;
    private NavigationItem _selectedNavItem = null!;

    public MainWindowViewModel()
    {
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem("Дашборд", "📊", new DashboardViewModel()),
            new NavigationItem("Работы", "🔧", new WorksViewModel()),
            new NavigationItem("Аварии", "⚠️", new AccidentsViewModel()),
            new NavigationItem("Передача смены", "🔄", new ShiftHandoverViewModel()),
            new NavigationItem("Отдел Г", "🏢", new DepartmentGViewModel()),
            new NavigationItem("МЗ", "📦", new MaterialsViewModel()),
            new NavigationItem("Заметки", "📝", new NotesViewModel()),
            new NavigationItem("Параметры", "⚙️", new SettingsViewModel())
        };

        SelectedNavItem = NavigationItems[0];
        CurrentPage = SelectedNavItem.ViewModel;

        NavigateCommand = ReactiveCommand.Create<NavigationItem>(Navigate);
    }

    public ObservableCollection<NavigationItem> NavigationItems { get; }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public NavigationItem SelectedNavItem
    {
        get => _selectedNavItem;
        set => this.RaiseAndSetIfChanged(ref _selectedNavItem, value);
    }

    public ReactiveCommand<NavigationItem, Unit> NavigateCommand { get; }

    private void Navigate(NavigationItem navItem)
    {
        SelectedNavItem = navItem;
        CurrentPage = navItem.ViewModel;
    }
}

public class NavigationItem
{
    public NavigationItem(string title, string icon, ViewModelBase viewModel)
    {
        Title = title;
        Icon = icon;
        ViewModel = viewModel;
    }

    public string Title { get; }
    public string Icon { get; }
    public ViewModelBase ViewModel { get; }
}