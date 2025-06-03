using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class StatusToColorConverter : IValueConverter
{
    public static readonly StatusToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "Выполнено" => new SolidColorBrush(Color.Parse("#10B981")),
                "В процессе" => new SolidColorBrush(Color.Parse("#F59E0B")),
                "Ожидает" => new SolidColorBrush(Color.Parse("#64748B")),
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