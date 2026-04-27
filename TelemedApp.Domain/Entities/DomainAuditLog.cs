namespace TelemedApp.Domain.Entities
{
    public class DomainAuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Entity { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty; // Create, Update, Delete
        public string PerformedBy { get; set; } = string.Empty;
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty;
    }
}