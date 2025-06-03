using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class Material
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [MaxLength(50)]
    public string? Category { get; set; }
    
    [MaxLength(50)]
    public string? Unit { get; set; }
    
    public decimal Quantity { get; set; } = 0;
    public decimal MinQuantity { get; set; } = 0;
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "В наличии";
    
    [MaxLength(100)]
    public string? Supplier { get; set; }
    
    public decimal? Price { get; set; }
    
    [MaxLength(100)]
    public string? Location { get; set; }
    
    public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
    
    [MaxLength(50)]
    public string? PartNumber { get; set; }
    
    public bool IsLowStock => Quantity <= MinQuantity;
}