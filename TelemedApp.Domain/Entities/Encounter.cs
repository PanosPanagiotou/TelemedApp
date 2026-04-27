namespace TelemedApp.Domain.Entities
{
    public class Encounter
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MedicalRecordId { get; set; }
        public Guid ProviderId { get; set; } // doctor or nurse
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string Procedures { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}