using Microsoft.EntityFrameworkCore;
using TrainDezhApp.Models;

namespace TrainDezhApp.Services;

public interface IDataService
{
    // Works
    Task<List<Work>> GetWorksAsync();
    Task<Work?> GetWorkByIdAsync(int id);
    Task<bool> AddWorkAsync(Work work);
    Task<bool> UpdateWorkAsync(Work work);
    Task<bool> DeleteWorkAsync(int id);

    // Accidents
    Task<List<Accident>> GetAccidentsAsync();
    Task<Accident?> GetAccidentByIdAsync(int id);
    Task<bool> AddAccidentAsync(Accident accident);
    Task<bool> UpdateAccidentAsync(Accident accident);
    Task<bool> DeleteAccidentAsync(int id);

    // Staff
    Task<List<Staff>> GetStaffAsync();
    Task<Staff?> GetStaffByIdAsync(int id);
    Task<bool> AddStaffAsync(Staff staff);
    Task<bool> UpdateStaffAsync(Staff staff);
    Task<bool> DeleteStaffAsync(int id);

    // Materials
    Task<List<Material>> GetMaterialsAsync();
    Task<Material?> GetMaterialByIdAsync(int id);
    Task<bool> AddMaterialAsync(Material material);
    Task<bool> UpdateMaterialAsync(Material material);
    Task<bool> DeleteMaterialAsync(int id);

    // Notes
    Task<List<Note>> GetNotesAsync();
    Task<Note?> GetNoteByIdAsync(int id);
    Task<bool> AddNoteAsync(Note note);
    Task<bool> UpdateNoteAsync(Note note);
    Task<bool> DeleteNoteAsync(int id);

    // Shift Handovers
    Task<List<ShiftHandover>> GetShiftHandoversAsync();
    Task<ShiftHandover?> GetShiftHandoverByIdAsync(int id);
    Task<bool> AddShiftHandoverAsync(ShiftHandover handover);
    Task<bool> UpdateShiftHandoverAsync(ShiftHandover handover);
    Task<bool> DeleteShiftHandoverAsync(int id);

    // Dashboard statistics
    Task<int> GetActiveWorksCountAsync();
    Task<int> GetCriticalAccidentsCountAsync();
    Task<int> GetStaffOnShiftCountAsync();
    Task<decimal> GetEquipmentReadinessAsync();
}

public class DataService : IDataService
{
    private readonly IDatabaseService _databaseService;

    public DataService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    private async Task<TrainDezhDbContext> GetContextAsync()
    {
        var settings = await _databaseService.GetSettingsAsync();
        var options = new DbContextOptionsBuilder<TrainDezhDbContext>()
            .UseNpgsql(settings.ConnectionString)
            .Options;
        return new TrainDezhDbContext(options);
    }

    // Works
    public async Task<List<Work>> GetWorksAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Works.OrderByDescending(w => w.CreatedAt).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения работ: {ex.Message}");
            return new List<Work>();
        }
    }

    public async Task<Work?> GetWorkByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Works.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения работы: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddWorkAsync(Work work)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Works.Add(work);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления работы: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateWorkAsync(Work work)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Works.Update(work);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления работы: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteWorkAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var work = await context.Works.FindAsync(id);
            if (work != null)
            {
                context.Works.Remove(work);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления работы: {ex.Message}");
            return false;
        }
    }

    // Accidents
    public async Task<List<Accident>> GetAccidentsAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Accidents.OrderByDescending(a => a.OccurredAt).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения аварий: {ex.Message}");
            return new List<Accident>();
        }
    }

    public async Task<Accident?> GetAccidentByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Accidents.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения аварии: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddAccidentAsync(Accident accident)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Accidents.Add(accident);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления аварии: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateAccidentAsync(Accident accident)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Accidents.Update(accident);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления аварии: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAccidentAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var accident = await context.Accidents.FindAsync(id);
            if (accident != null)
            {
                context.Accidents.Remove(accident);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления аварии: {ex.Message}");
            return false;
        }
    }

    // Staff
    public async Task<List<Staff>> GetStaffAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Staff.OrderBy(s => s.LastName).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения персонала: {ex.Message}");
            return new List<Staff>();
        }
    }

    public async Task<Staff?> GetStaffByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Staff.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения сотрудника: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddStaffAsync(Staff staff)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Staff.Add(staff);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления сотрудника: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateStaffAsync(Staff staff)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Staff.Update(staff);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления сотрудника: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteStaffAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var staff = await context.Staff.FindAsync(id);
            if (staff != null)
            {
                context.Staff.Remove(staff);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления сотрудника: {ex.Message}");
            return false;
        }
    }

    // Materials
    public async Task<List<Material>> GetMaterialsAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Materials.OrderBy(m => m.Name).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения материалов: {ex.Message}");
            return new List<Material>();
        }
    }

    public async Task<Material?> GetMaterialByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Materials.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения материала: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddMaterialAsync(Material material)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Materials.Add(material);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления материала: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateMaterialAsync(Material material)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Materials.Update(material);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления материала: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteMaterialAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var material = await context.Materials.FindAsync(id);
            if (material != null)
            {
                context.Materials.Remove(material);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления материала: {ex.Message}");
            return false;
        }
    }

    // Notes
    public async Task<List<Note>> GetNotesAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения заметок: {ex.Message}");
            return new List<Note>();
        }
    }

    public async Task<Note?> GetNoteByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Notes.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения заметки: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddNoteAsync(Note note)
    {
        try
        {
            using var context = await GetContextAsync();
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления заметки: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateNoteAsync(Note note)
    {
        try
        {
            using var context = await GetContextAsync();
            note.UpdatedAt = DateTime.UtcNow;
            context.Notes.Update(note);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления заметки: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var note = await context.Notes.FindAsync(id);
            if (note != null)
            {
                context.Notes.Remove(note);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления заметки: {ex.Message}");
            return false;
        }
    }

    // Shift Handovers
    public async Task<List<ShiftHandover>> GetShiftHandoversAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.ShiftHandovers.OrderByDescending(sh => sh.ShiftDate).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения передач смен: {ex.Message}");
            return new List<ShiftHandover>();
        }
    }

    public async Task<ShiftHandover?> GetShiftHandoverByIdAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.ShiftHandovers.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения передачи смены: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddShiftHandoverAsync(ShiftHandover handover)
    {
        try
        {
            using var context = await GetContextAsync();
            context.ShiftHandovers.Add(handover);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления передачи смены: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateShiftHandoverAsync(ShiftHandover handover)
    {
        try
        {
            using var context = await GetContextAsync();
            context.ShiftHandovers.Update(handover);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления передачи смены: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteShiftHandoverAsync(int id)
    {
        try
        {
            using var context = await GetContextAsync();
            var handover = await context.ShiftHandovers.FindAsync(id);
            if (handover != null)
            {
                context.ShiftHandovers.Remove(handover);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления передачи смены: {ex.Message}");
            return false;
        }
    }

    // Dashboard statistics
    public async Task<int> GetActiveWorksCountAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Works.CountAsync(w => w.Status == "Выполняется" || w.Status == "Новая");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения количества активных работ: {ex.Message}");
            return 0;
        }
    }

    public async Task<int> GetCriticalAccidentsCountAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Accidents.CountAsync(a => a.Severity == "Критическая" && a.Status != "Устранена");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения количества критических аварий: {ex.Message}");
            return 0;
        }
    }

    public async Task<int> GetStaffOnShiftCountAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            return await context.Staff.CountAsync(s => s.IsOnShift);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения количества сотрудников на смене: {ex.Message}");
            return 0;
        }
    }

    public async Task<decimal> GetEquipmentReadinessAsync()
    {
        try
        {
            using var context = await GetContextAsync();
            var totalWorks = await context.Works.CountAsync();
            var completedWorks = await context.Works.CountAsync(w => w.Status == "Завершено");
            
            if (totalWorks == 0) return 100m;
            
            return Math.Round((decimal)completedWorks / totalWorks * 100, 1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения готовности оборудования: {ex.Message}");
            return 0m;
        }
    }
}