using TelemedApp.Domain.Enums;

namespace TelemedApp.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DoctorStatus Status { get; set; } = DoctorStatus.Active;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}