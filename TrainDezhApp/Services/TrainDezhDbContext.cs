using Microsoft.EntityFrameworkCore;
using TrainDezhApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace TrainDezhApp.Services;

public class TrainDezhDbContext : DbContext
{
    public DbSet<Work> Works { get; set; }
    public DbSet<Accident> Accidents { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<ShiftHandover> ShiftHandovers { get; set; }
    public DbSet<DatabaseSettings> DatabaseSettings { get; set; }
    public DbSet<User> Users { get; set; }

    public TrainDezhDbContext(DbContextOptions<TrainDezhDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка таблиц
        modelBuilder.Entity<Work>(entity =>
        {
            entity.ToTable("works");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Priority);
        });

        modelBuilder.Entity<Accident>(entity =>
        {
            entity.ToTable("accidents");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ReportedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Severity);
            entity.HasIndex(e => e.Status);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("staff");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HiredAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.EmployeeNumber).IsUnique();
            entity.HasIndex(e => e.Status);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("materials");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.Quantity).HasPrecision(10, 2);
            entity.Property(e => e.MinQuantity).HasPrecision(10, 2);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Category);
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("notes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Priority);
        });

        modelBuilder.Entity<ShiftHandover>(entity =>
        {
            entity.ToTable("shift_handovers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HandoverTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.ShiftDate);
            entity.HasIndex(e => e.ShiftType);
        });

        modelBuilder.Entity<DatabaseSettings>(entity =>
        {
            entity.ToTable("database_settings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        // Начальные данные
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Настройки БД по умолчанию
        modelBuilder.Entity<DatabaseSettings>().HasData(
            new DatabaseSettings
            {
                Id = 1,
                Host = "localhost",
                Port = 5432,
                Database = "train_dezh",
                Username = "postgres",
                Password = "",
                UseSSL = false,
                ConnectionTimeout = 30
            }
        );

        // Тестовые данные для персонала
        modelBuilder.Entity<Staff>().HasData(
            new Staff { Id = 1, FirstName = "Иван", LastName = "Петров", Position = "Дежурный по станции", EmployeeNumber = "001", IsOnShift = true, Department = "Отдел Г" },
            new Staff { Id = 2, FirstName = "Мария", LastName = "Сидорова", Position = "Машинист", EmployeeNumber = "002", IsOnShift = true, Department = "Отдел Г" },
            new Staff { Id = 3, FirstName = "Алексей", LastName = "Козлов", Position = "Слесарь", EmployeeNumber = "003", IsOnShift = false, Department = "Отдел Г" }
        );

        // Тестовые работы
        modelBuilder.Entity<Work>().HasData(
            new Work { Id = 1, Title = "Плановое ТО локомотива №1234", Status = "Выполняется", Priority = "Средний", AssignedTo = "Козлов А.", Equipment = "Локомотив №1234" },
            new Work { Id = 2, Title = "Замена тормозных колодок", Status = "Завершено", Priority = "Низкий", AssignedTo = "Сидорова М.", Equipment = "Вагон №5678" },
            new Work { Id = 3, Title = "Ремонт светофора", Status = "Новая", Priority = "Высокий", Location = "км 25" }
        );

        // Тестовые аварии
        modelBuilder.Entity<Accident>().HasData(
            new Accident { Id = 1, Description = "Сход вагона на км 15", Severity = "Критическая", Status = "Расследуется", Location = "км 15" },
            new Accident { Id = 2, Description = "Неисправность светофора", Severity = "Средняя", Status = "Устранена", Location = "км 25", Equipment = "Светофор №12" }
        );

        // Тестовые материалы
        modelBuilder.Entity<Material>().HasData(
            new Material { Id = 1, Name = "Тормозные колодки", Category = "Запчасти", Unit = "шт", Quantity = 15, MinQuantity = 5, Status = "В наличии", Price = 2500.00m },
            new Material { Id = 2, Name = "Масло моторное", Category = "ГСМ", Unit = "л", Quantity = 3, MinQuantity = 10, Status = "Заканчивается", Price = 450.00m },
            new Material { Id = 3, Name = "Болты М12", Category = "Крепеж", Unit = "шт", Quantity = 100, MinQuantity = 20, Status = "В наличии", Price = 15.00m }
        );

        // Пользователи по умолчанию
        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1, 
                FullName = "Администратор", 
                Username = "root", 
                PasswordHash = HashPassword("admin123"), 
                Role = UserRole.Administrator, 
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}