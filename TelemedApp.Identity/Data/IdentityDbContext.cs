using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Data
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) :
            IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    }
}