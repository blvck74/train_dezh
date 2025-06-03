using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class MaterialsViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Material> Materials { get; }
    public ReactiveCommand<Unit, Unit> AddMaterialCommand { get; }
    public ReactiveCommand<Material, Unit> EditMaterialCommand { get; }
    public ReactiveCommand<Material, Unit> DeleteMaterialCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public MaterialsViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        Materials = new ObservableCollection<Material>();

        AddMaterialCommand = ReactiveCommand.CreateFromTask(AddMaterial);
        EditMaterialCommand = ReactiveCommand.CreateFromTask<Material>(EditMaterial);
        DeleteMaterialCommand = ReactiveCommand.CreateFromTask<Material>(DeleteMaterial);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadMaterials);

        // Загружаем данные при инициализации
        _ = LoadMaterials();
    }

    private async Task LoadMaterials()
    {
        try
        {
            var materials = await _dataService.GetMaterialsAsync();
            Materials.Clear();
            foreach (var material in materials)
            {
                Materials.Add(material);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            Materials.Clear();
            var testMaterials = new[]
            {
                new Material { Id = 1, Name = "Насос центробежный", Quantity = 12, Unit = "шт", Status = "В наличии" },
                new Material { Id = 2, Name = "Трубы стальные Ø100", Quantity = 250, Unit = "м", Status = "В наличии" },
                new Material { Id = 3, Name = "Кабель силовой", Quantity = 50, Unit = "м", Status = "Мало" },
                new Material { Id = 4, Name = "Датчики давления", Quantity = 3, Unit = "шт", Status = "Заказано" }
            };
            
            foreach (var material in testMaterials)
            {
                Materials.Add(material);
            }
        }
    }

    private async Task AddMaterial()
    {
        try
        {
            var newMaterial = new Material
            {
                Name = "Новый материал",
                Quantity = 0,
                Unit = "шт",
                Status = "В наличии"
            };

            var success = await _dataService.AddMaterialAsync(newMaterial);
            if (success)
            {
                await LoadMaterials(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task EditMaterial(Material material)
    {
        try
        {
            await _dataService.UpdateMaterialAsync(material);
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task DeleteMaterial(Material material)
    {
        try
        {
            await _dataService.DeleteMaterialAsync(material.Id);
            Materials.Remove(material);
        }
        catch
        {
            // Обработка ошибки
        }
    }
}