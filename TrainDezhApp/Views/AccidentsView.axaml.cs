using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Views;

public partial class AccidentsView : UserControl
{
    public AccidentsView()
    {
        InitializeComponent();
    }
}

public class SeverityToColorConverter : IValueConverter
{
    public static readonly SeverityToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "Критический" => new SolidColorBrush(Color.Parse("#DC2626")),
            "Высокий" => new SolidColorBrush(Color.Parse("#EF4444")),
            "Средний" => new SolidColorBrush(Color.Parse("#F59E0B")),
            "Низкий" => new SolidColorBrush(Color.Parse("#10B981")),
            _ => new SolidColorBrush(Color.Parse("#64748B"))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class AccidentStatusToColorConverter : IValueConverter
{
    public static readonly AccidentStatusToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "Активная" => new SolidColorBrush(Color.Parse("#EF4444")),
            "В работе" => new SolidColorBrush(Color.Parse("#F59E0B")),
            "Устранена" => new SolidColorBrush(Color.Parse("#10B981")),
            _ => new SolidColorBrush(Color.Parse("#64748B"))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}