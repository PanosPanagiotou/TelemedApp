namespace TelemedApp.Application.DTOs
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public Guid MedicalRecordId { get; set; }
        public string Medication { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
    }
}