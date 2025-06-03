using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TrainDezhApp.ViewModels;
using TrainDezhApp.Views;
using TrainDezhApp.Services;

namespace TrainDezhApp;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _serviceProvider = Program.ConfigureServices();
            ShowLoginWindow(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private async void ShowLoginWindow(IClassicDesktopStyleApplicationLifetime desktop)
    {
        var userService = _serviceProvider!.GetRequiredService<IUserService>();
        var loginViewModel = new LoginViewModel(userService);
        var loginWindow = new LoginWindow(loginViewModel);

        var result = await loginWindow.ShowDialog<object?>(null!);

        if (result != null)
        {
            // Пользователь успешно авторизован
            var mainViewModel = _serviceProvider!.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            desktop.MainWindow.Show();
        }
        else
        {
            // Пользователь закрыл окно авторизации без входа
            desktop.Shutdown();
        }
    }
}