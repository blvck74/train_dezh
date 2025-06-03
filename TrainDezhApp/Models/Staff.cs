using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class Staff
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? MiddleName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Position { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Phone { get; set; }
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Активен";
    
    [MaxLength(50)]
    public string? Department { get; set; }
    
    public DateTime HiredAt { get; set; } = DateTime.UtcNow;
    
    [MaxLength(20)]
    public string? EmployeeNumber { get; set; }
    
    public bool IsOnShift { get; set; } = false;
    
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}