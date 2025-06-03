using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using TrainDezhApp.Services;
using TrainDezhApp.Models;

namespace TrainDezhApp.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading = false;

    public LoginViewModel(IUserService userService)
    {
        _userService = userService;

        var canLogin = this.WhenAnyValue(
            x => x.Username,
            x => x.Password,
            x => x.IsLoading,
            (username, password, isLoading) => 
                !string.IsNullOrWhiteSpace(username) && 
                !string.IsNullOrWhiteSpace(password) && 
                !isLoading);

        LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync, canLogin);
        
        LoginCommand.ThrownExceptions.Subscribe(ex =>
        {
            ErrorMessage = "Ошибка при входе в систему";
            IsLoading = false;
        });
    }

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    public ReactiveCommand<Unit, User?> LoginCommand { get; }

    private async Task<User?> LoginAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var user = await _userService.AuthenticateAsync(Username, Password);
            
            if (user == null)
            {
                ErrorMessage = "Неверный логин или пароль";
                return null;
            }

            CurrentUser.Instance = user;
            return user;
        }
        catch (Exception)
        {
            ErrorMessage = "Ошибка при подключении к базе данных";
            return null;
        }
        finally
        {
            IsLoading = false;
        }
    }
}