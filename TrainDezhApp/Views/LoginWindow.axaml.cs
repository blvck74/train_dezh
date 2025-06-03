using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainDezhApp.ViewModels;
using TrainDezhApp.Models;

namespace TrainDezhApp.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    public LoginWindow(LoginViewModel viewModel) : this()
    {
        DataContext = viewModel;
        
        // Подписываемся на успешную авторизацию
        viewModel.LoginCommand.Subscribe(user =>
        {
            if (user != null)
            {
                Close(user);
            }
        });
    }
}