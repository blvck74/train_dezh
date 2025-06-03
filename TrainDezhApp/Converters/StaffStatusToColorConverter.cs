using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class StaffStatusToColorConverter : IValueConverter
{
    public static readonly StaffStatusToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "На смене" => new SolidColorBrush(Color.Parse("#10B981")),
                "Отдыхает" => new SolidColorBrush(Color.Parse("#64748B")),
                "Больничный" => new SolidColorBrush(Color.Parse("#F59E0B")),
                "Отпуск" => new SolidColorBrush(Color.Parse("#3B82F6")),
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