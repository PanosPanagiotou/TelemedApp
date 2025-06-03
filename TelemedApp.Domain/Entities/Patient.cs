namespace TelemedApp.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = [];
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
    }
}
