namespace TelemedApp.Domain.Entities
{
    public class BillingLine
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}