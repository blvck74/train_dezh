using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class AccidentStatusToColorConverter : IValueConverter
{
    public static readonly AccidentStatusToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "Устранена" => new SolidColorBrush(Color.Parse("#10B981")),
                "В работе" => new SolidColorBrush(Color.Parse("#F59E0B")),
                "Новая" => new SolidColorBrush(Color.Parse("#EF4444")),
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