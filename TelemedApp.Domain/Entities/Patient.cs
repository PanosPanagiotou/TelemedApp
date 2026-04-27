using TelemedApp.Domain.Enums;

namespace TelemedApp.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? MaidenName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsuranceNo { get; set; } = string.Empty;
        public string MedicalRecordNumber { get; set; } = string.Empty;
        public string NextOfKin { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public PatientStatus Status { get; set; } = PatientStatus.Active;
        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
    }
}