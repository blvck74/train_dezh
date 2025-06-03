using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Views;

public partial class WorksView : UserControl
{
    public WorksView()
    {
        InitializeComponent();
    }
}

public class StatusToColorConverter : IValueConverter
{
    public static readonly StatusToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "В процессе" => new SolidColorBrush(Color.Parse("#2563EB")),
            "Запланировано" => new SolidColorBrush(Color.Parse("#64748B")),
            "Завершено" => new SolidColorBrush(Color.Parse("#10B981")),
            _ => new SolidColorBrush(Color.Parse("#64748B"))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PriorityToColorConverter : IValueConverter
{
    public static readonly PriorityToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
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