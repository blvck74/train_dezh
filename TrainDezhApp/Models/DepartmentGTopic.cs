namespace TrainDezhApp.Models;

public class DepartmentGTopic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<DepartmentGSubtopic> Subtopics { get; set; } = new();
}

public class DepartmentGSubtopic
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<DepartmentGContent> Contents { get; set; } = new();
}

public class DepartmentGContent
{
    public int Id { get; set; }
    public int SubtopicId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public ContentType Type { get; set; } = ContentType.Text;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public List<ContentLink> Links { get; set; } = new();
}

public class ContentLink
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public enum ContentType
{
    Text,
    Image,
    Link,
    Mixed
}

public enum NavigationLevel
{
    Topics,
    Subtopics,
    Content
}