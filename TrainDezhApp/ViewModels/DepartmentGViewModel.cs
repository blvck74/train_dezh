using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

namespace TrainDezhApp.ViewModels;

public class DepartmentGViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    // Навигация
    private NavigationLevel _currentLevel = NavigationLevel.Topics;
    private DepartmentGTopic? _selectedTopic;
    private DepartmentGSubtopic? _selectedSubtopic;
    private DepartmentGContent? _selectedContent;
    
    // Коллекции данных
    public ObservableCollection<DepartmentGTopic> Topics { get; }
    public ObservableCollection<DepartmentGSubtopic> Subtopics { get; }
    public ObservableCollection<DepartmentGContent> Contents { get; }
    
    // Редактор текста
    private string _editorContent = string.Empty;
    private bool _isEditing = false;
    private bool _isBold = false;
    private bool _isItalic = false;
    private bool _isUnderline = false;
    private string _textColor = "#000000";
    private string _textAlign = "Left";
    
    // Команды навигации
    public ReactiveCommand<DepartmentGTopic, Unit> SelectTopicCommand { get; }
    public ReactiveCommand<DepartmentGSubtopic, Unit> SelectSubtopicCommand { get; }
    public ReactiveCommand<DepartmentGContent, Unit> SelectContentCommand { get; }
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
    
    // Команды редактора
    public ReactiveCommand<Unit, Unit> StartEditingCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveContentCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelEditingCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleBoldCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleItalicCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleUnderlineCommand { get; }
    public ReactiveCommand<string, Unit> SetTextColorCommand { get; }
    public ReactiveCommand<string, Unit> SetTextAlignCommand { get; }
    public ReactiveCommand<Unit, Unit> InsertLinkCommand { get; }
    public ReactiveCommand<Unit, Unit> InsertImageCommand { get; }

    public DepartmentGViewModel(IDataService? dataService = null)
    {
        _dataService = dataService ?? new DataService(new DatabaseService());
        
        Topics = new ObservableCollection<DepartmentGTopic>();
        Subtopics = new ObservableCollection<DepartmentGSubtopic>();
        Contents = new ObservableCollection<DepartmentGContent>();

        // Команды навигации
        SelectTopicCommand = ReactiveCommand.Create<DepartmentGTopic>(SelectTopic);
        SelectSubtopicCommand = ReactiveCommand.Create<DepartmentGSubtopic>(SelectSubtopic);
        SelectContentCommand = ReactiveCommand.Create<DepartmentGContent>(SelectContent);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        
        // Команды редактора
        StartEditingCommand = ReactiveCommand.Create(StartEditing);
        SaveContentCommand = ReactiveCommand.CreateFromTask(SaveContent);
        CancelEditingCommand = ReactiveCommand.Create(CancelEditing);
        ToggleBoldCommand = ReactiveCommand.Create(ToggleBold);
        ToggleItalicCommand = ReactiveCommand.Create(ToggleItalic);
        ToggleUnderlineCommand = ReactiveCommand.Create(ToggleUnderline);
        SetTextColorCommand = ReactiveCommand.Create<string>(SetTextColor);
        SetTextAlignCommand = ReactiveCommand.Create<string>(SetTextAlign);
        InsertLinkCommand = ReactiveCommand.Create(InsertLink);
        InsertImageCommand = ReactiveCommand.Create(InsertImage);

        // Загружаем данные при инициализации
        LoadTopics();
    }
    
    // Свойства для привязки
    public NavigationLevel CurrentLevel
    {
        get => _currentLevel;
        set => this.RaiseAndSetIfChanged(ref _currentLevel, value);
    }
    
    public DepartmentGTopic? SelectedTopic
    {
        get => _selectedTopic;
        set => this.RaiseAndSetIfChanged(ref _selectedTopic, value);
    }
    
    public DepartmentGSubtopic? SelectedSubtopic
    {
        get => _selectedSubtopic;
        set => this.RaiseAndSetIfChanged(ref _selectedSubtopic, value);
    }
    
    public DepartmentGContent? SelectedContent
    {
        get => _selectedContent;
        set => this.RaiseAndSetIfChanged(ref _selectedContent, value);
    }
    
    public string EditorContent
    {
        get => _editorContent;
        set => this.RaiseAndSetIfChanged(ref _editorContent, value);
    }
    
    public bool IsEditing
    {
        get => _isEditing;
        set => this.RaiseAndSetIfChanged(ref _isEditing, value);
    }
    
    public bool IsBold
    {
        get => _isBold;
        set => this.RaiseAndSetIfChanged(ref _isBold, value);
    }
    
    public bool IsItalic
    {
        get => _isItalic;
        set => this.RaiseAndSetIfChanged(ref _isItalic, value);
    }
    
    public bool IsUnderline
    {
        get => _isUnderline;
        set => this.RaiseAndSetIfChanged(ref _isUnderline, value);
    }
    
    public string TextColor
    {
        get => _textColor;
        set => this.RaiseAndSetIfChanged(ref _textColor, value);
    }
    
    public string TextAlign
    {
        get => _textAlign;
        set => this.RaiseAndSetIfChanged(ref _textAlign, value);
    }
    
    // Вычисляемые свойства для видимости
    public bool IsTopicsVisible => CurrentLevel == NavigationLevel.Topics;
    public bool IsSubtopicsVisible => CurrentLevel == NavigationLevel.Subtopics;
    public bool IsContentVisible => CurrentLevel == NavigationLevel.Content;
    public bool CanGoBack => CurrentLevel != NavigationLevel.Topics;
    
    // Методы навигации
    private void SelectTopic(DepartmentGTopic topic)
    {
        SelectedTopic = topic;
        CurrentLevel = NavigationLevel.Subtopics;
        
        Subtopics.Clear();
        foreach (var subtopic in topic.Subtopics)
        {
            Subtopics.Add(subtopic);
        }
        
        this.RaisePropertyChanged(nameof(IsTopicsVisible));
        this.RaisePropertyChanged(nameof(IsSubtopicsVisible));
        this.RaisePropertyChanged(nameof(IsContentVisible));
        this.RaisePropertyChanged(nameof(CanGoBack));
    }
    
    private void SelectSubtopic(DepartmentGSubtopic subtopic)
    {
        SelectedSubtopic = subtopic;
        CurrentLevel = NavigationLevel.Content;
        
        Contents.Clear();
        foreach (var content in subtopic.Contents)
        {
            Contents.Add(content);
        }
        
        this.RaisePropertyChanged(nameof(IsTopicsVisible));
        this.RaisePropertyChanged(nameof(IsSubtopicsVisible));
        this.RaisePropertyChanged(nameof(IsContentVisible));
        this.RaisePropertyChanged(nameof(CanGoBack));
    }
    
    private void SelectContent(DepartmentGContent content)
    {
        SelectedContent = content;
        EditorContent = content.Content;
        IsEditing = false;
    }
    
    private void GoBack()
    {
        switch (CurrentLevel)
        {
            case NavigationLevel.Subtopics:
                CurrentLevel = NavigationLevel.Topics;
                SelectedTopic = null;
                break;
            case NavigationLevel.Content:
                CurrentLevel = NavigationLevel.Subtopics;
                SelectedSubtopic = null;
                SelectedContent = null;
                break;
        }
        
        this.RaisePropertyChanged(nameof(IsTopicsVisible));
        this.RaisePropertyChanged(nameof(IsSubtopicsVisible));
        this.RaisePropertyChanged(nameof(IsContentVisible));
        this.RaisePropertyChanged(nameof(CanGoBack));
    }
    
    // Методы редактора
    private void StartEditing()
    {
        IsEditing = true;
    }
    
    private Task SaveContent()
    {
        if (SelectedContent != null)
        {
            SelectedContent.Content = EditorContent;
            SelectedContent.UpdatedAt = DateTime.Now;
            // Здесь можно добавить сохранение в базу данных
            IsEditing = false;
        }
        return Task.CompletedTask;
    }
    
    private void CancelEditing()
    {
        if (SelectedContent != null)
        {
            EditorContent = SelectedContent.Content;
        }
        IsEditing = false;
    }
    
    private void ToggleBold()
    {
        IsBold = !IsBold;
        ApplyFormatting();
    }
    
    private void ToggleItalic()
    {
        IsItalic = !IsItalic;
        ApplyFormatting();
    }
    
    private void ToggleUnderline()
    {
        IsUnderline = !IsUnderline;
        ApplyFormatting();
    }
    
    private void SetTextColor(string color)
    {
        TextColor = color;
        ApplyFormatting();
    }
    
    private void SetTextAlign(string align)
    {
        TextAlign = align;
        ApplyFormatting();
    }
    
    private void InsertLink()
    {
        // Здесь можно открыть диалог для вставки ссылки
        var linkText = "[Текст ссылки](http://example.com)";
        EditorContent += linkText;
    }
    
    private void InsertImage()
    {
        // Здесь можно открыть диалог для выбора изображения
        var imageText = "![Описание изображения](path/to/image.jpg)";
        EditorContent += imageText;
    }
    
    private void ApplyFormatting()
    {
        // Здесь можно применить форматирование к выделенному тексту
        // В реальном приложении это будет более сложная логика
    }
    
    private void LoadTopics()
    {
        // Загружаем тестовые данные
        var testTopics = new[]
        {
            new DepartmentGTopic
            {
                Id = 1,
                Name = "Безопасность труда",
                Icon = "🛡️",
                Description = "Правила и инструкции по безопасности",
                Subtopics = new List<DepartmentGSubtopic>
                {
                    new DepartmentGSubtopic
                    {
                        Id = 1,
                        TopicId = 1,
                        Name = "Общие правила",
                        Icon = "📋",
                        Description = "Основные правила безопасности",
                        Contents = new List<DepartmentGContent>
                        {
                            new DepartmentGContent
                            {
                                Id = 1,
                                SubtopicId = 1,
                                Title = "Инструкция по ТБ",
                                Content = "Основные правила техники безопасности на рабочем месте...",
                                Type = ContentType.Text
                            }
                        }
                    },
                    new DepartmentGSubtopic
                    {
                        Id = 2,
                        TopicId = 1,
                        Name = "Средства защиты",
                        Icon = "🦺",
                        Description = "Индивидуальные средства защиты",
                        Contents = new List<DepartmentGContent>
                        {
                            new DepartmentGContent
                            {
                                Id = 2,
                                SubtopicId = 2,
                                Title = "СИЗ на производстве",
                                Content = "Перечень и правила использования средств индивидуальной защиты...",
                                Type = ContentType.Text
                            }
                        }
                    }
                }
            },
            new DepartmentGTopic
            {
                Id = 2,
                Name = "Технологические процессы",
                Icon = "⚙️",
                Description = "Описание производственных процессов",
                Subtopics = new List<DepartmentGSubtopic>
                {
                    new DepartmentGSubtopic
                    {
                        Id = 3,
                        TopicId = 2,
                        Name = "Основные операции",
                        Icon = "🔧",
                        Description = "Стандартные технологические операции",
                        Contents = new List<DepartmentGContent>
                        {
                            new DepartmentGContent
                            {
                                Id = 3,
                                SubtopicId = 3,
                                Title = "Порядок выполнения работ",
                                Content = "Последовательность выполнения основных технологических операций...",
                                Type = ContentType.Text
                            }
                        }
                    }
                }
            },
            new DepartmentGTopic
            {
                Id = 3,
                Name = "Документооборот",
                Icon = "📄",
                Description = "Ведение документации и отчётности",
                Subtopics = new List<DepartmentGSubtopic>
                {
                    new DepartmentGSubtopic
                    {
                        Id = 4,
                        TopicId = 3,
                        Name = "Отчёты",
                        Icon = "📊",
                        Description = "Формы отчётности",
                        Contents = new List<DepartmentGContent>
                        {
                            new DepartmentGContent
                            {
                                Id = 4,
                                SubtopicId = 4,
                                Title = "Ежедневные отчёты",
                                Content = "Форма и порядок заполнения ежедневных отчётов...",
                                Type = ContentType.Text
                            }
                        }
                    }
                }
            },
            new DepartmentGTopic
            {
                Id = 4,
                Name = "Обучение персонала",
                Icon = "🎓",
                Description = "Материалы для обучения сотрудников",
                Subtopics = new List<DepartmentGSubtopic>
                {
                    new DepartmentGSubtopic
                    {
                        Id = 5,
                        TopicId = 4,
                        Name = "Новые сотрудники",
                        Icon = "👤",
                        Description = "Программа адаптации",
                        Contents = new List<DepartmentGContent>
                        {
                            new DepartmentGContent
                            {
                                Id = 5,
                                SubtopicId = 5,
                                Title = "План адаптации",
                                Content = "Программа адаптации новых сотрудников отдела...",
                                Type = ContentType.Text
                            }
                        }
                    }
                }
            }
        };
        
        Topics.Clear();
        foreach (var topic in testTopics)
        {
            Topics.Add(topic);
        }
    }


}