using Microsoft.EntityFrameworkCore;
using TrainDezhApp.Models;
using TrainDezhApp.Services;

Console.WriteLine("=== ДЕМОНСТРАЦИЯ СИСТЕМЫ АВТОРИЗАЦИИ ===\n");

// Создаем in-memory базу данных для демонстрации
var options = new DbContextOptionsBuilder<TrainDezhDbContext>()
    .UseInMemoryDatabase(databaseName: "DemoDatabase")
    .Options;

using var context = new TrainDezhDbContext(options);
var userService = new UserService(context);

// Добавляем администратора
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
await context.SaveChangesAsync();

Console.WriteLine("1. Тестируем авторизацию администратора:");
var admin = await userService.AuthenticateAsync("root", "admin123");
if (admin != null)
{
    Console.WriteLine($"✓ Успешная авторизация: {admin.FullName} ({admin.Role})");
    CurrentUser.Instance = admin;
}
else
{
    Console.WriteLine("✗ Ошибка авторизации");
}

Console.WriteLine("\n2. Тестируем неверные учетные данные:");
var invalidUser = await userService.AuthenticateAsync("root", "wrongpassword");
if (invalidUser == null)
{
    Console.WriteLine("✓ Неверные учетные данные корректно отклонены");
}

Console.WriteLine("\n3. Создаем нового пользователя:");
var newUser = await userService.CreateUserAsync("Иван Петров", "ivan", UserRole.User);
Console.WriteLine($"✓ Создан пользователь: {newUser.FullName}");
Console.WriteLine($"  Логин: {newUser.Username}");
Console.WriteLine($"  Роль: {newUser.Role}");

Console.WriteLine("\n4. Получаем список всех пользователей:");
var allUsers = await userService.GetAllUsersAsync();
foreach (var user in allUsers)
{
    Console.WriteLine($"  - {user.FullName} ({user.Username}) - {user.Role}");
}

Console.WriteLine("\n5. Проверяем права доступа:");
Console.WriteLine($"  Текущий пользователь: {CurrentUser.Instance?.FullName}");
Console.WriteLine($"  Является администратором: {CurrentUser.IsAdministrator}");
Console.WriteLine($"  Может создавать пользователей: {CurrentUser.IsAdministrator}");

Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
