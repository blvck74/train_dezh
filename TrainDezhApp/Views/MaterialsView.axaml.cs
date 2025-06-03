using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace TrainDezhApp.Views;

public partial class MaterialsView : UserControl
{
    public MaterialsView()
    {
        InitializeComponent();
    }
}

public class MaterialStatusToColorConverter : IValueConverter
{
    public static readonly MaterialStatusToColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() switch
        {
            "В наличии" => new SolidColorBrush(Color.Parse("#10B981")),
            "Мало" => new SolidColorBrush(Color.Parse("#F59E0B")),
            "Заказано" => new SolidColorBrush(Color.Parse("#2563EB")),
            "Нет в наличии" => new SolidColorBrush(Color.Parse("#EF4444")),
            _ => new SolidColorBrush(Color.Parse("#64748B"))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}