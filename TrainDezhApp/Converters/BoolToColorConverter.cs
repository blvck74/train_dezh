using Avalonia.Data.Converters;
using System.Globalization;

namespace TrainDezhApp.Converters;

public class BoolToColorConverter : IValueConverter
{
    public static readonly BoolToColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "#10B981" : "#F59E0B"; // Зеленый для завершено, желтый для в процессе
        }
        return "#6B7280"; // Серый для неизвестно
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}