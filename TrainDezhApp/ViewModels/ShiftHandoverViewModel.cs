using ReactiveUI;

namespace TrainDezhApp.ViewModels;

public class ShiftHandoverViewModel : ViewModelBase
{
    private string _currentShift = "Смена А (08:00 - 20:00)";
    private string _nextShift = "Смена Б (20:00 - 08:00)";
    private string _handoverNotes = "";

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
}