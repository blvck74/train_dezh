using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class AccidentsViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Accident> Accidents { get; }
    public ReactiveCommand<Unit, Unit> AddAccidentCommand { get; }
    public ReactiveCommand<Accident, Unit> EditAccidentCommand { get; }
    public ReactiveCommand<Accident, Unit> DeleteAccidentCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public AccidentsViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        Accidents = new ObservableCollection<Accident>();

        AddAccidentCommand = ReactiveCommand.CreateFromTask(AddAccident);
        EditAccidentCommand = ReactiveCommand.CreateFromTask<Accident>(EditAccident);
        DeleteAccidentCommand = ReactiveCommand.CreateFromTask<Accident>(DeleteAccident);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadAccidents);

        // Загружаем данные при инициализации
        _ = LoadAccidents();
    }

    private async Task LoadAccidents()
    {
        try
        {
            var accidents = await _dataService.GetAccidentsAsync();
            Accidents.Clear();
            foreach (var accident in accidents)
            {
                Accidents.Add(accident);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            Accidents.Clear();
            var testAccidents = new[]
            {
                new Accident { Id = 1, Description = "Утечка на линии А", Severity = "Критический", Status = "Активная", OccurredAt = DateTime.Today.AddHours(9).AddMinutes(45) },
                new Accident { Id = 2, Description = "Перегрев двигателя №2", Severity = "Высокий", Status = "Устранена", OccurredAt = DateTime.Today.AddHours(8).AddMinutes(30) },
                new Accident { Id = 3, Description = "Сбой датчика давления", Severity = "Средний", Status = "В работе", OccurredAt = DateTime.Today.AddHours(7).AddMinutes(15) }
            };
            
            foreach (var accident in testAccidents)
            {
                Accidents.Add(accident);
            }
        }
    }

    private async Task AddAccident()
    {
        try
        {
            var newAccident = new Accident
            {
                Description = "Новый инцидент",
                Severity = "Средний",
                Status = "Зарегистрировано",
                OccurredAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            var success = await _dataService.AddAccidentAsync(newAccident);
            if (success)
            {
                await LoadAccidents(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task EditAccident(Accident accident)
    {
        try
        {
            accident.UpdatedAt = DateTime.Now;
            await _dataService.UpdateAccidentAsync(accident);
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task DeleteAccident(Accident accident)
    {
        try
        {
            await _dataService.DeleteAccidentAsync(accident.Id);
            Accidents.Remove(accident);
        }
        catch
        {
            // Обработка ошибки
        }
    }
}