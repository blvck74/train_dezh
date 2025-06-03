using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Services;
using TrainDezhApp.Models;

namespace TrainDezhApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentPage = null!;
    private NavigationItem _selectedNavItem = null!;
    private readonly IUserService _userService;

    public MainWindowViewModel(IUserService userService)
    {
        _userService = userService;
        
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

        // Добавляем вкладку управления пользователями только для администраторов
        if (CurrentUser.IsAdministrator)
        {
            NavigationItems.Add(new NavigationItem("Пользователи", "👥", new UserManagementViewModel(_userService)));
        }

        SelectedNavItem = NavigationItems[0];
        CurrentPage = SelectedNavItem.ViewModel;

        NavigateCommand = ReactiveCommand.Create<NavigationItem>(Navigate);
    }

    // Конструктор без параметров для дизайнера
    public MainWindowViewModel() : this(null!)
    {
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

    public string CurrentUserName => CurrentUser.Instance?.FullName ?? "Неизвестный пользователь";
    
    public string CurrentUserRole => CurrentUser.Instance?.Role switch
    {
        UserRole.Administrator => "Администратор",
        UserRole.User => "Пользователь",
        _ => "Неизвестная роль"
    };

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