namespace TelemedApp.Identity.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string Action { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}