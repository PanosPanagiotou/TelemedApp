using TelemedApp.Domain.Enums;

namespace TelemedApp.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public AppointmentStatus Status { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}