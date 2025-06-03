using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Views;

public partial class DepartmentGView : UserControl
{
    public DepartmentGView()
    {
        InitializeComponent();
    }
}

public class StaffStatusToColorConverter : IValueConverter
{
    public static readonly StaffStatusToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "На смене" => new SolidColorBrush(Color.Parse("#10B981")),
            "Отпуск" => new SolidColorBrush(Color.Parse("#F59E0B")),
            "Больничный" => new SolidColorBrush(Color.Parse("#EF4444")),
            _ => new SolidColorBrush(Color.Parse("#64748B"))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}