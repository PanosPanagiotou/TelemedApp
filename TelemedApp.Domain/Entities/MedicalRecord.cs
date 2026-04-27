namespace TelemedApp.Domain.Entities
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Encounter> Encounters { get; set; } = [];
        public string Notes { get; set; } = string.Empty;
        public Patient? Patient { get; set; }
    }
}
