using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class BoolToBackgroundConverter : IValueConverter
{
    public static readonly BoolToBackgroundConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isEditing)
        {
            return isEditing ? Brushes.White : new SolidColorBrush(Color.Parse("#F8FAFC"));
        }
        return new SolidColorBrush(Color.Parse("#F8FAFC"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}