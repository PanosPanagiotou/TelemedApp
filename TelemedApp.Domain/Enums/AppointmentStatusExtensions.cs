namespace TelemedApp.Domain.Enums
{
    public static class AppointmentStatusExtensions
    {
        public static string ToDisplay(this AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Scheduled => "Scheduled",
                AppointmentStatus.Completed => "Completed",
                AppointmentStatus.Cancelled => "Cancelled",
                AppointmentStatus.NoShow => "No-show",
                _ => status.ToString()
            };
        }
    }
}