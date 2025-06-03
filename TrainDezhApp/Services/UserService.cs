using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TrainDezhApp.Models;

namespace TrainDezhApp.Services;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<User> CreateUserAsync(string fullName, string username, UserRole role);
    Task<List<User>> GetAllUsersAsync();
    Task<bool> IsUsernameAvailableAsync(string username);
    string GenerateRandomPassword();
}

public class UserService : IUserService
{
    private readonly TrainDezhDbContext _context;

    public UserService(TrainDezhDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        if (user == null)
            return null;

        var passwordHash = HashPassword(password);
        return user.PasswordHash == passwordHash ? user : null;
    }

    public async Task<User> CreateUserAsync(string fullName, string username, UserRole role)
    {
        var password = GenerateRandomPassword();
        var passwordHash = HashPassword(password);

        var user = new User
        {
            FullName = fullName,
            Username = username,
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Временно сохраняем пароль в объекте для возврата
        user.PasswordHash = password; // Возвращаем исходный пароль для показа администратору
        
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    public async Task<bool> IsUsernameAvailableAsync(string username)
    {
        return !await _context.Users
            .AnyAsync(u => u.Username == username && u.IsActive);
    }

    public string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}

public static class CurrentUser
{
    public static User? Instance { get; set; }
    
    public static bool IsAuthenticated => Instance != null;
    
    public static bool IsAdministrator => Instance?.Role == UserRole.Administrator;
}