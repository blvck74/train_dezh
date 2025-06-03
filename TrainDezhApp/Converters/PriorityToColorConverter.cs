using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class PriorityToColorConverter : IValueConverter
{
    public static readonly PriorityToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string priority)
        {
            return priority switch
            {
                "Высокий" => new SolidColorBrush(Color.Parse("#EF4444")),
                "Средний" => new SolidColorBrush(Color.Parse("#F59E0B")),
                "Низкий" => new SolidColorBrush(Color.Parse("#10B981")),
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