using TelemedApp.UI.Client.Models;

namespace TelemedApp.UI.Client.Services
{
    public class DashboardSummary
    {
        public int TotalPatients { get; set; }
        public int ActiveDoctors { get; set; }
        public int AppointmentsToday { get; set; }
        public int PendingLabResults { get; set; }
        public IEnumerable<AppointmentViewModel> UpcomingAppointments { get; set; } = [];
    }
}