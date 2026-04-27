namespace TelemedApp.Domain.Entities
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Medication { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public Guid PatientId { get; set; }
        public Guid PrescribedById { get; set; } // doctor id
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PrescriptionItem> Items { get; set; } = [];

        public string Notes { get; set; } = string.Empty;
        public Appointment? Appointment { get; set; }
    }
}
