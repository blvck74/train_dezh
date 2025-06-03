using System.Collections.ObjectModel;

namespace TrainDezhApp.ViewModels;

public class WorksViewModel : ViewModelBase
{
    public WorksViewModel()
    {
        Works = new ObservableCollection<WorkItemViewModel>
        {
            new WorkItemViewModel("Замена насоса №3", "В процессе", "Высокий", "Иванов И.И."),
            new WorkItemViewModel("Проверка электрооборудования", "Запланировано", "Средний", "Петров П.П."),
            new WorkItemViewModel("Ремонт трубопровода", "Завершено", "Низкий", "Сидоров С.С."),
            new WorkItemViewModel("Калибровка датчиков", "В процессе", "Средний", "Козлов К.К.")
        };
    }

    public ObservableCollection<WorkItemViewModel> Works { get; }
}

public class WorkItemViewModel
{
    public WorkItemViewModel(string title, string status, string priority, string assignee)
    {
        Title = title;
        Status = status;
        Priority = priority;
        Assignee = assignee;
    }

    public string Title { get; }
    public string Status { get; }
    public string Priority { get; }
    public string Assignee { get; }
}