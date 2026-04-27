namespace TelemedApp.Domain.Entities
{
    public class BillingInvoice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public List<BillingLine> Lines { get; set; } = [];
        public decimal Total => Lines.Sum(l => l.Amount);
        public string Status { get; set; } = "Unpaid"; // Unpaid, Paid, Partial
    }
}