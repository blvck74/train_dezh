using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class SeverityToColorConverter : IValueConverter
{
    public static readonly SeverityToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string severity)
        {
            return severity switch
            {
                "Критическая" => new SolidColorBrush(Color.Parse("#EF4444")),
                "Высокая" => new SolidColorBrush(Color.Parse("#F59E0B")),
                "Средняя" => new SolidColorBrush(Color.Parse("#10B981")),
                "Низкая" => new SolidColorBrush(Color.Parse("#64748B")),
                _ => new SolidColorBrush(Color.Parse("#64748B"))
            };
        }
        return new SolidColorBrush(Color.Parse("#64748B"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}