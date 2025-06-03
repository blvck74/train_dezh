using System.ComponentModel.DataAnnotations;

namespace TrainDezhApp.Models;

public class DatabaseSettings
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Host { get; set; } = "localhost";
    
    public int Port { get; set; } = 5432;
    
    [Required]
    [MaxLength(100)]
    public string Database { get; set; } = "train_dezh";
    
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = "postgres";
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    
    public bool UseSSL { get; set; } = false;
    
    public int ConnectionTimeout { get; set; } = 30;
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    public string ConnectionString => 
        $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};" +
        $"SSL Mode={(UseSSL ? "Require" : "Disable")};Timeout={ConnectionTimeout};";
}