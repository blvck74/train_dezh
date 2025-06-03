using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TrainDezhApp.Services;

public class TrainDezhDbContextFactory : IDesignTimeDbContextFactory<TrainDezhDbContext>
{
    public TrainDezhDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TrainDezhDbContext>();
        
        // Используем строку подключения по умолчанию для миграций
        var connectionString = "Host=localhost;Port=5432;Database=train_dezh;Username=postgres;Password=;SSL Mode=Disable;Timeout=30;";
        
        optionsBuilder.UseNpgsql(connectionString);

        return new TrainDezhDbContext(optionsBuilder.Options);
    }
}