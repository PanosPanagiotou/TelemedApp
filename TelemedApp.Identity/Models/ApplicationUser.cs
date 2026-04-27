using Microsoft.AspNetCore.Identity;

namespace TelemedApp.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<string> UserFeatureFlags { get; set; } = [];
        // Marking password changing by date
        public DateTime PasswordLastChangedAt { get; set; } = DateTime.UtcNow;
    }
}