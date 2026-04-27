using TelemedApp.Domain.Enums;

namespace TelemedApp.Application.DTOs
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DoctorStatus Status { get; set; } = DoctorStatus.Active;
    }
}