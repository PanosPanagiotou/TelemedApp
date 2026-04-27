namespace TelemedApp.Domain.Entities
{
    public class FinanceRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = string.Empty; // Invoice, Payment, Refund
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";
        public string Notes { get; set; } = string.Empty;
    }
}