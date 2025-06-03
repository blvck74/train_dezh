using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class WorksViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Work> Works { get; }
    public ReactiveCommand<Unit, Unit> AddWorkCommand { get; }
    public ReactiveCommand<Work, Unit> EditWorkCommand { get; }
    public ReactiveCommand<Work, Unit> DeleteWorkCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public WorksViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        Works = new ObservableCollection<Work>();

        AddWorkCommand = ReactiveCommand.CreateFromTask(AddWork);
        EditWorkCommand = ReactiveCommand.CreateFromTask<Work>(EditWork);
        DeleteWorkCommand = ReactiveCommand.CreateFromTask<Work>(DeleteWork);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadWorks);

        // Загружаем данные при инициализации
        _ = LoadWorks();
    }

    private async Task LoadWorks()
    {
        try
        {
            var works = await _dataService.GetWorksAsync();
            Works.Clear();
            foreach (var work in works)
            {
                Works.Add(work);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            Works.Clear();
            var testWorks = new[]
            {
                new Work { Id = 1, Title = "Замена насоса №3", Status = "В процессе", Priority = "Высокий", AssignedTo = "Иванов И.И.", DueDate = DateTime.Today.AddDays(1) },
                new Work { Id = 2, Title = "Проверка электрооборудования", Status = "Запланировано", Priority = "Средний", AssignedTo = "Петров П.П.", DueDate = DateTime.Today.AddDays(3) },
                new Work { Id = 3, Title = "Ремонт трубопровода", Status = "Завершено", Priority = "Низкий", AssignedTo = "Сидоров С.С.", DueDate = DateTime.Today.AddDays(-1) },
                new Work { Id = 4, Title = "Калибровка датчиков", Status = "В процессе", Priority = "Средний", AssignedTo = "Козлов К.К.", DueDate = DateTime.Today.AddDays(2) }
            };
            
            foreach (var work in testWorks)
            {
                Works.Add(work);
            }
        }
    }

    private async Task AddWork()
    {
        try
        {
            var newWork = new Work
            {
                Title = "Новая работа",
                Status = "Запланировано",
                Priority = "Средний",
                AssignedTo = "Не назначено",
                DueDate = DateTime.Today.AddDays(7),
                CreatedAt = DateTime.Now
            };

            var success = await _dataService.AddWorkAsync(newWork);
            if (success)
            {
                await LoadWorks(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task EditWork(Work work)
    {
        try
        {
            work.UpdatedAt = DateTime.Now;
            await _dataService.UpdateWorkAsync(work);
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task DeleteWork(Work work)
    {
        try
        {
            await _dataService.DeleteWorkAsync(work.Id);
            Works.Remove(work);
        }
        catch
        {
            // Обработка ошибки
        }
    }
}