using Microsoft.EntityFrameworkCore;
using TrainDezhApp.Models;
using TrainDezhApp.Services;
using Xunit;

namespace TrainDezhApp.Tests;

public class UserServiceTests
{
    private TrainDezhDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TrainDezhDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var context = new TrainDezhDbContext(options);
        
        // Добавляем тестового администратора
        context.Users.Add(new User
        {
            Id = 1,
            FullName = "Администратор",
            Username = "root",
            PasswordHash = UserService.HashPassword("admin123"),
            Role = UserRole.Administrator,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        });
        
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task AuthenticateAsync_ValidCredentials_ReturnsUser()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var userService = new UserService(context);

        // Act
        var user = await userService.AuthenticateAsync("root", "admin123");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("root", user.Username);
        Assert.Equal(UserRole.Administrator, user.Role);
    }

    [Fact]
    public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var userService = new UserService(context);

        // Act
        var user = await userService.AuthenticateAsync("root", "wrongpassword");

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task CreateUserAsync_ValidData_CreatesUser()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var userService = new UserService(context);

        // Act
        var user = await userService.CreateUserAsync("Иван Иванов", "ivan", UserRole.User);

        // Assert
        Assert.NotNull(user);
        Assert.Equal("Иван Иванов", user.FullName);
        Assert.Equal("ivan", user.Username);
        Assert.Equal(UserRole.User, user.Role);
        Assert.NotNull(user.PasswordHash); // В тесте это будет сгенерированный пароль
        Assert.True(user.PasswordHash.Length >= 8);
    }

    [Fact]
    public void HashPassword_SamePassword_ReturnsSameHash()
    {
        // Arrange
        var password = "testpassword";

        // Act
        var hash1 = UserService.HashPassword(password);
        var hash2 = UserService.HashPassword(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "testpassword";
        var hash = UserService.HashPassword(password);

        // Act
        var result = UserService.VerifyPassword(password, hash);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_IncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var password = "testpassword";
        var wrongPassword = "wrongpassword";
        var hash = UserService.HashPassword(password);

        // Act
        var result = UserService.VerifyPassword(wrongPassword, hash);

        // Assert
        Assert.False(result);
    }
}