using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class Accident
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Details { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Severity { get; set; } = "Низкая";
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Зарегистрирована";
    
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    [MaxLength(100)]
    public string? ReportedBy { get; set; }
    
    [MaxLength(100)]
    public string? Location { get; set; }
    
    [MaxLength(100)]
    public string? Equipment { get; set; }
    
    [MaxLength(1000)]
    public string? Investigation { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
}