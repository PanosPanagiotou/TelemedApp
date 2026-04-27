namespace TelemedApp.Shared.Authorization
{
    public static class Support
    {
        public const string ViewPatients = "Support.ViewPatients";
        public const string ViewAppointments = "Support.ViewAppointments";
        public const string RescheduleAppointments = "Support.RescheduleAppointments";

        public static IEnumerable<string> All() =>
        [
            ViewPatients, ViewAppointments, RescheduleAppointments
        ];
    }
}