using System.Collections.ObjectModel;
using ReactiveUI;

namespace TrainDezhApp.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private string _newNoteText = "";

    public NotesViewModel()
    {
        Notes = new ObservableCollection<NoteViewModel>
        {
            new NoteViewModel("Проверить состояние насоса №3 до конца смены", "10:30"),
            new NoteViewModel("Заказать запчасти для ремонта", "09:15"),
            new NoteViewModel("Провести инструктаж новых сотрудников", "08:45")
        };
    }

    public ObservableCollection<NoteViewModel> Notes { get; }

    public string NewNoteText
    {
        get => _newNoteText;
        set => this.RaiseAndSetIfChanged(ref _newNoteText, value);
    }
}

public class NoteViewModel
{
    public NoteViewModel(string text, string time)
    {
        Text = text;
        Time = time;
    }

    public string Text { get; }
    public string Time { get; }
}