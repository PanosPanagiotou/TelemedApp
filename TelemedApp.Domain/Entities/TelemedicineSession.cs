namespace TelemedApp.Domain.Entities
{
    public class TelemedicineSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AppointmentId { get; set; }
        public string SessionUrl { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public string RecordingUrl { get; set; } = string.Empty;
    }
}