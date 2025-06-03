using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrainDezhApp.Services;

namespace TrainDezhApp;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // STA-dependent code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();

    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        return services.BuildServiceProvider();
    }
}
