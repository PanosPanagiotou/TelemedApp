namespace TelemedApp.Domain.Entities
{
    public class Claim
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PolicyId { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Submitted"; // Submitted, Processed, Paid, Denied
        public decimal Amount { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}