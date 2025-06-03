using Avalonia.Data.Converters;
using System.Globalization;
using TrainDezhApp.Models;

namespace TrainDezhApp.Converters;

public class UserRoleConverter : IValueConverter
{
    public static readonly UserRoleConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is UserRole role)
        {
            return role switch
            {
                UserRole.Administrator => "Администратор",
                UserRole.User => "Пользователь",
                _ => "Неизвестно"
            };
        }

        return "Неизвестно";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}