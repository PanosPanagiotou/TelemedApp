namespace TelemedApp.Domain.Entities
{
    public class InsurancePolicy
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}