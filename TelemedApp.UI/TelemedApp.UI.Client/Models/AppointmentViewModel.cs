using TelemedApp.Domain.Enums;

namespace TelemedApp.UI.Client.Models
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}