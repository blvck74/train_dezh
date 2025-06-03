using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class ShiftHandoverViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private string _currentShift = "Смена А (08:00 - 20:00)";
    private string _nextShift = "Смена Б (20:00 - 08:00)";
    private string _handoverNotes = "";
    
    public ObservableCollection<ShiftHandover> ShiftHandovers { get; }
    public ReactiveCommand<Unit, Unit> CreateHandoverCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public ShiftHandoverViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        ShiftHandovers = new ObservableCollection<ShiftHandover>();

        CreateHandoverCommand = ReactiveCommand.CreateFromTask(CreateHandover);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadShiftHandovers);

        // Загружаем данные при инициализации
        _ = LoadShiftHandovers();
    }

    public string CurrentShift
    {
        get => _currentShift;
        set => this.RaiseAndSetIfChanged(ref _currentShift, value);
    }

    public string NextShift
    {
        get => _nextShift;
        set => this.RaiseAndSetIfChanged(ref _nextShift, value);
    }

    public string HandoverNotes
    {
        get => _handoverNotes;
        set => this.RaiseAndSetIfChanged(ref _handoverNotes, value);
    }

    private async Task LoadShiftHandovers()
    {
        try
        {
            var handovers = await _dataService.GetShiftHandoversAsync();
            ShiftHandovers.Clear();
            foreach (var handover in handovers.OrderByDescending(h => h.HandoverTime))
            {
                ShiftHandovers.Add(handover);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            ShiftHandovers.Clear();
            var testHandovers = new[]
            {
                new ShiftHandover 
                { 
                    Id = 1, 
                    ShiftType = "День", 
                    HandoverTime = DateTime.Now.AddHours(-8),
                    ImportantNotes = "Все оборудование в рабочем состоянии. Проведена проверка тормозной системы.",
                    OutgoingOfficer = "Иванов И.И.",
                    IncomingOfficer = "Петров П.П.",
                    IsCompleted = true
                },
                new ShiftHandover 
                { 
                    Id = 2, 
                    ShiftType = "Ночь", 
                    HandoverTime = DateTime.Now.AddHours(-20),
                    ImportantNotes = "Требуется внимание к насосу №3. Заменены фильтры в системе вентиляции.",
                    OutgoingOfficer = "Петров П.П.",
                    IncomingOfficer = "Сидоров С.С.",
                    IsCompleted = true
                }
            };
            
            foreach (var handover in testHandovers)
            {
                ShiftHandovers.Add(handover);
            }
        }
    }

    private async Task CreateHandover()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(HandoverNotes))
                return;

            var newHandover = new ShiftHandover
            {
                ShiftType = CurrentShift.Contains("День") ? "День" : "Ночь",
                HandoverTime = DateTime.Now,
                ImportantNotes = HandoverNotes,
                OutgoingOfficer = "Текущий пользователь", // В реальном приложении получать из контекста пользователя
                IncomingOfficer = "Следующая смена",
                IsCompleted = false
            };

            var success = await _dataService.AddShiftHandoverAsync(newHandover);
            if (success)
            {
                HandoverNotes = ""; // Очищаем поле ввода
                await LoadShiftHandovers(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }
}