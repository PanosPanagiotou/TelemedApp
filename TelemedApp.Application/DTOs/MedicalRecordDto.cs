namespace TelemedApp.Application.DTOs
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}