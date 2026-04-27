using TelemedApp.Identity.Models;

namespace TelemedApp.Application.Interfaces
{
    public interface IAuditService
    {
        Task LogAsync(string userId, string action, string description, string? ip, string? agent);
        Task<List<AuditLog>> GetAllAsync();
    }
}
