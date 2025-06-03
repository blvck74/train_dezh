using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class BoolToBorderConverter : IValueConverter
{
    public static readonly BoolToBorderConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isEditing)
        {
            return isEditing ? new SolidColorBrush(Color.Parse("#2563EB")) : new SolidColorBrush(Color.Parse("#E2E8F0"));
        }
        return new SolidColorBrush(Color.Parse("#E2E8F0"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}