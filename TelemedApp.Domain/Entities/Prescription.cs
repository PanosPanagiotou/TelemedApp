namespace TelemedApp.Domain.Entities
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Medication { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;

        public Appointment? Appointment { get; set; }
    }
}
