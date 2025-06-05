namespace TelemedApp.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Status { get; set; }
    }
}
