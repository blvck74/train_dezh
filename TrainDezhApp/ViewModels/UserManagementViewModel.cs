using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using TrainDezhApp.Services;
using TrainDezhApp.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace TrainDezhApp.ViewModels;

public class UserManagementViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    private ObservableCollection<User> _users = new();
    private bool _isAddUserDialogOpen = false;
    private string _newUserFullName = string.Empty;
    private string _newUserUsername = string.Empty;
    private UserRole _newUserRole = UserRole.User;
    private string _generatedPassword = string.Empty;
    private bool _isPasswordGenerated = false;
    private string _errorMessage = string.Empty;

    public UserManagementViewModel(IUserService userService)
    {
        _userService = userService;

        var canAddUser = this.WhenAnyValue(
            x => x.NewUserFullName,
            x => x.NewUserUsername,
            (fullName, username) => 
                !string.IsNullOrWhiteSpace(fullName) && 
                !string.IsNullOrWhiteSpace(username));

        AddUserCommand = ReactiveCommand.Create(OpenAddUserDialog);
        CreateUserCommand = ReactiveCommand.CreateFromTask(CreateUserAsync, canAddUser);
        CancelAddUserCommand = ReactiveCommand.Create(CancelAddUser);
        CopyToClipboardCommand = ReactiveCommand.CreateFromTask<string>(CopyToClipboardAsync);
        RefreshUsersCommand = ReactiveCommand.CreateFromTask(LoadUsersAsync);

        // Загружаем пользователей при инициализации
        _ = LoadUsersAsync();
    }

    public ObservableCollection<User> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }

    public bool IsAddUserDialogOpen
    {
        get => _isAddUserDialogOpen;
        set => this.RaiseAndSetIfChanged(ref _isAddUserDialogOpen, value);
    }

    public string NewUserFullName
    {
        get => _newUserFullName;
        set => this.RaiseAndSetIfChanged(ref _newUserFullName, value);
    }

    public string NewUserUsername
    {
        get => _newUserUsername;
        set => this.RaiseAndSetIfChanged(ref _newUserUsername, value);
    }

    public UserRole NewUserRole
    {
        get => _newUserRole;
        set => this.RaiseAndSetIfChanged(ref _newUserRole, value);
    }

    public string GeneratedPassword
    {
        get => _generatedPassword;
        set => this.RaiseAndSetIfChanged(ref _generatedPassword, value);
    }

    public bool IsPasswordGenerated
    {
        get => _isPasswordGenerated;
        set => this.RaiseAndSetIfChanged(ref _isPasswordGenerated, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> CreateUserCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelAddUserCommand { get; }
    public ReactiveCommand<string, Unit> CopyToClipboardCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshUsersCommand { get; }

    private void OpenAddUserDialog()
    {
        ResetAddUserForm();
        IsAddUserDialogOpen = true;
    }

    private async Task CreateUserAsync()
    {
        try
        {
            ErrorMessage = string.Empty;

            // Проверяем доступность логина
            if (!await _userService.IsUsernameAvailableAsync(NewUserUsername))
            {
                ErrorMessage = "Пользователь с таким логином уже существует";
                return;
            }

            var user = await _userService.CreateUserAsync(NewUserFullName, NewUserUsername, NewUserRole);
            GeneratedPassword = user.PasswordHash; // Временно содержит исходный пароль
            IsPasswordGenerated = true;

            // Обновляем список пользователей
            await LoadUsersAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при создании пользователя: {ex.Message}";
        }
    }

    private void CancelAddUser()
    {
        IsAddUserDialogOpen = false;
        ResetAddUserForm();
    }

    private void ResetAddUserForm()
    {
        NewUserFullName = string.Empty;
        NewUserUsername = string.Empty;
        NewUserRole = UserRole.User;
        GeneratedPassword = string.Empty;
        IsPasswordGenerated = false;
        ErrorMessage = string.Empty;
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при загрузке пользователей: {ex.Message}";
        }
    }

    private async Task CopyToClipboardAsync(string text)
    {
        try
        {
            var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            var topLevel = desktop?.MainWindow;
            if (topLevel?.Clipboard != null)
            {
                await topLevel.Clipboard.SetTextAsync(text);
            }
        }
        catch
        {
            // Игнорируем ошибки копирования
        }
    }

    public string GetRoleDisplayName(UserRole role)
    {
        return role switch
        {
            UserRole.Administrator => "Администратор",
            UserRole.User => "Пользователь",
            _ => "Неизвестно"
        };
    }
}