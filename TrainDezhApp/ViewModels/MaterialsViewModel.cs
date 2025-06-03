using System.Collections.ObjectModel;

namespace TrainDezhApp.ViewModels;

public class MaterialsViewModel : ViewModelBase
{
    public MaterialsViewModel()
    {
        Materials = new ObservableCollection<MaterialViewModel>
        {
            new MaterialViewModel("Насос центробежный", "12", "шт", "В наличии"),
            new MaterialViewModel("Трубы стальные Ø100", "250", "м", "В наличии"),
            new MaterialViewModel("Кабель силовой", "50", "м", "Мало"),
            new MaterialViewModel("Датчики давления", "3", "шт", "Заказано")
        };
    }

    public ObservableCollection<MaterialViewModel> Materials { get; }
}

public class MaterialViewModel
{
    public MaterialViewModel(string name, string quantity, string unit, string status)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        Status = status;
    }

    public string Name { get; }
    public string Quantity { get; }
    public string Unit { get; }
    public string Status { get; }
}