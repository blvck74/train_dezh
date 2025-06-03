using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class ShiftHandover
{
    public int Id { get; set; }
    
    public DateTime ShiftDate { get; set; } = DateTime.Today;
    
    [Required]
    [MaxLength(50)]
    public string ShiftType { get; set; } = "День"; // День, Ночь
    
    [MaxLength(100)]
    public string? OutgoingOfficer { get; set; }
    
    [MaxLength(100)]
    public string? IncomingOfficer { get; set; }
    
    [MaxLength(2000)]
    public string? CurrentTasks { get; set; }
    
    [MaxLength(2000)]
    public string? ImportantNotes { get; set; }
    
    [MaxLength(2000)]
    public string? EquipmentStatus { get; set; }
    
    [MaxLength(2000)]
    public string? SafetyIssues { get; set; }
    
    public DateTime HandoverTime { get; set; } = DateTime.UtcNow;
    
    public bool IsCompleted { get; set; } = false;
    
    [MaxLength(100)]
    public string? Signature { get; set; }
}