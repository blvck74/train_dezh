using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class DepartmentGViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Staff> Staff { get; }
    public ReactiveCommand<Unit, Unit> AddStaffCommand { get; }
    public ReactiveCommand<Staff, Unit> EditStaffCommand { get; }
    public ReactiveCommand<Staff, Unit> DeleteStaffCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public DepartmentGViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        Staff = new ObservableCollection<Staff>();

        AddStaffCommand = ReactiveCommand.CreateFromTask(AddStaff);
        EditStaffCommand = ReactiveCommand.CreateFromTask<Staff>(EditStaff);
        DeleteStaffCommand = ReactiveCommand.CreateFromTask<Staff>(DeleteStaff);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadStaff);

        // Загружаем данные при инициализации
        _ = LoadStaff();
    }

    private async Task LoadStaff()
    {
        try
        {
            var staff = await _dataService.GetStaffAsync();
            Staff.Clear();
            foreach (var member in staff)
            {
                Staff.Add(member);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            Staff.Clear();
            var testStaff = new[]
            {
                new Staff { Id = 1, FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович", Position = "Начальник смены", Status = "На смене", Department = "Отдел Г" },
                new Staff { Id = 2, FirstName = "Петр", LastName = "Петров", MiddleName = "Петрович", Position = "Слесарь", Status = "На смене", Department = "Отдел Г" },
                new Staff { Id = 3, FirstName = "Сидор", LastName = "Сидоров", MiddleName = "Сидорович", Position = "Электрик", Status = "Отпуск", Department = "Отдел Г" },
                new Staff { Id = 4, FirstName = "Козьма", LastName = "Козлов", MiddleName = "Козьмич", Position = "Оператор", Status = "На смене", Department = "Отдел Г" }
            };
            
            foreach (var member in testStaff)
            {
                Staff.Add(member);
            }
        }
    }

    private async Task AddStaff()
    {
        try
        {
            var newStaff = new Staff
            {
                FirstName = "Новый",
                LastName = "Сотрудник",
                Position = "Должность",
                Status = "Активен",
                Department = "Отдел Г"
            };

            var success = await _dataService.AddStaffAsync(newStaff);
            if (success)
            {
                await LoadStaff(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task EditStaff(Staff staff)
    {
        try
        {
            await _dataService.UpdateStaffAsync(staff);
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task DeleteStaff(Staff staff)
    {
        try
        {
            await _dataService.DeleteStaffAsync(staff.Id);
            Staff.Remove(staff);
        }
        catch
        {
            // Обработка ошибки
        }
    }
}