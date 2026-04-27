namespace TelemedApp.Domain.Entities
{
    public class Referral
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FromProviderId { get; set; }
        public Guid ToProviderId { get; set; }
        public Guid PatientId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}