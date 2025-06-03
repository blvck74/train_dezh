using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private string _newNoteText = "";
    
    public ObservableCollection<Note> Notes { get; }
    public ReactiveCommand<Unit, Unit> AddNoteCommand { get; }
    public ReactiveCommand<Note, Unit> EditNoteCommand { get; }
    public ReactiveCommand<Note, Unit> DeleteNoteCommand { get; }
    public ReactiveCommand<Unit, Unit> RefreshCommand { get; }

    public NotesViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        Notes = new ObservableCollection<Note>();

        AddNoteCommand = ReactiveCommand.CreateFromTask(AddNote);
        EditNoteCommand = ReactiveCommand.CreateFromTask<Note>(EditNote);
        DeleteNoteCommand = ReactiveCommand.CreateFromTask<Note>(DeleteNote);
        RefreshCommand = ReactiveCommand.CreateFromTask(LoadNotes);

        // Загружаем данные при инициализации
        _ = LoadNotes();
    }

    public string NewNoteText
    {
        get => _newNoteText;
        set => this.RaiseAndSetIfChanged(ref _newNoteText, value);
    }

    private async Task LoadNotes()
    {
        try
        {
            var notes = await _dataService.GetNotesAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }
        catch
        {
            // Fallback к тестовым данным
            Notes.Clear();
            var testNotes = new[]
            {
                new Note { Id = 1, Content = "Проверить состояние насоса №3 до конца смены", Priority = "Высокий", CreatedAt = DateTime.Now.AddHours(-2) },
                new Note { Id = 2, Content = "Заказать запчасти для ремонта", Priority = "Средний", CreatedAt = DateTime.Now.AddHours(-3) },
                new Note { Id = 3, Content = "Провести инструктаж новых сотрудников", Priority = "Низкий", CreatedAt = DateTime.Now.AddHours(-4) }
            };
            
            foreach (var note in testNotes)
            {
                Notes.Add(note);
            }
        }
    }

    private async Task AddNote()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NewNoteText))
                return;

            var newNote = new Note
            {
                Content = NewNoteText,
                Priority = "Средний",
                CreatedAt = DateTime.Now
            };

            var success = await _dataService.AddNoteAsync(newNote);
            if (success)
            {
                NewNoteText = ""; // Очищаем поле ввода
                await LoadNotes(); // Перезагружаем данные
            }
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task EditNote(Note note)
    {
        try
        {
            await _dataService.UpdateNoteAsync(note);
        }
        catch
        {
            // Обработка ошибки
        }
    }

    private async Task DeleteNote(Note note)
    {
        try
        {
            await _dataService.DeleteNoteAsync(note.Id);
            Notes.Remove(note);
        }
        catch
        {
            // Обработка ошибки
        }
    }
}