using Microsoft.Extensions.DependencyInjection;
using TrainDezhApp.ViewModels;

namespace TrainDezhApp.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Регистрируем сервисы
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddSingleton<IDataService, DataService>();
        services.AddScoped<IUserService, UserService>();
        
        // Регистрируем ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<UserManagementViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<WorksViewModel>();
        services.AddTransient<AccidentsViewModel>();
        services.AddTransient<ShiftHandoverViewModel>();
        services.AddTransient<DepartmentGViewModel>();
        services.AddTransient<MaterialsViewModel>();
        services.AddTransient<NotesViewModel>();
        services.AddTransient<SettingsViewModel>();
        
        return services;
    }
}