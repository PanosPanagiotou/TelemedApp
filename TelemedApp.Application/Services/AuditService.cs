using Microsoft.EntityFrameworkCore;
using TelemedApp.Identity.Models;
using TelemedApp.Identity.Data;
using TelemedApp.Application.Interfaces;

namespace TelemedApp.Application.Services
{
    public class AuditService(IdentityDbContext db) : IAuditService
    {
        private readonly IdentityDbContext _db = db;

        public async Task LogAsync(string userId, string action, string description, string? ip, string? agent)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Description = description,
                IpAddress = ip,
                UserAgent = agent
            };

            _db.AuditLogs.Add(log);
            await _db.SaveChangesAsync();
        }
        public async Task<List<AuditLog>> GetAllAsync()
            => await _db.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
    }
}
