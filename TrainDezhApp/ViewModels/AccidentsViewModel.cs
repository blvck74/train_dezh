using System.Collections.ObjectModel;

namespace TrainDezhApp.ViewModels;

public class AccidentsViewModel : ViewModelBase
{
    public AccidentsViewModel()
    {
        Accidents = new ObservableCollection<AccidentViewModel>
        {
            new AccidentViewModel("Утечка на линии А", "Критический", "09:45", "Активная"),
            new AccidentViewModel("Перегрев двигателя №2", "Высокий", "08:30", "Устранена"),
            new AccidentViewModel("Сбой датчика давления", "Средний", "07:15", "В работе")
        };
    }

    public ObservableCollection<AccidentViewModel> Accidents { get; }
}

public class AccidentViewModel
{
    public AccidentViewModel(string description, string severity, string time, string status)
    {
        Description = description;
        Severity = severity;
        Time = time;
        Status = status;
    }

    public string Description { get; }
    public string Severity { get; }
    public string Time { get; }
    public string Status { get; }
}