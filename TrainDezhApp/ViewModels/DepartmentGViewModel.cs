using System.Collections.ObjectModel;

namespace TrainDezhApp.ViewModels;

public class DepartmentGViewModel : ViewModelBase
{
    public DepartmentGViewModel()
    {
        Staff = new ObservableCollection<StaffMemberViewModel>
        {
            new StaffMemberViewModel("Иванов Иван Иванович", "Начальник смены", "На смене"),
            new StaffMemberViewModel("Петров Петр Петрович", "Слесарь", "На смене"),
            new StaffMemberViewModel("Сидоров Сидор Сидорович", "Электрик", "Отпуск"),
            new StaffMemberViewModel("Козлов Козьма Козьмич", "Оператор", "На смене")
        };
    }

    public ObservableCollection<StaffMemberViewModel> Staff { get; }
}

public class StaffMemberViewModel
{
    public StaffMemberViewModel(string name, string position, string status)
    {
        Name = name;
        Position = position;
        Status = status;
    }

    public string Name { get; }
    public string Position { get; }
    public string Status { get; }
}