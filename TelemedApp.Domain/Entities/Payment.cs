namespace TelemedApp.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string Method { get; set; } = "Card"; // Card, Cash, Transfer
    }
}