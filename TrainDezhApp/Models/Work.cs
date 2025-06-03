using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class Work
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Новая";
    
    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = "Средний";
    
    [MaxLength(100)]
    public string? AssignedTo { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    
    [MaxLength(100)]
    public string? Equipment { get; set; }
    
    [MaxLength(50)]
    public string? Location { get; set; }
}