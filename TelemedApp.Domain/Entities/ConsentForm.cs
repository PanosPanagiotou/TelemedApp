namespace TelemedApp.Domain.Entities
{
    public class ConsentForm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime SignedAt { get; set; }
        public Guid DocumentId { get; set; }
    }
}