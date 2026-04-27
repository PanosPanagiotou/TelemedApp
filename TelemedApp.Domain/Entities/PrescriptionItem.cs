namespace TelemedApp.Domain.Entities
{
    public class PrescriptionItem
    {
        public Guid Id { get; set; }
        public Guid PrescriptionId { get; set; }
        public Guid PharmacyItemId { get; set; }
        public string? Dosage { get; set; }
        public int Quantity { get; set; }
    }
}