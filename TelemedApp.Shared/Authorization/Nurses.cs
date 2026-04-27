namespace TelemedApp.Shared.Authorization
{
    public static class Nurses
    {
        public const string ViewPatients = "Nurses.ViewPatients";
        public const string ViewAppointments = "Nurses.ViewAppointments";
        public const string EditAppointments = "Nurses.EditAppointments";
        public const string RescheduleAppointments = "Nurses.RescheduleAppointments";
        public const string ViewMedicalRecords = "Nurses.ViewMedicalRecords";
        public const string UpdateVitals = "Nurses.UpdateVitals";

        public static IEnumerable<string> All() =>
        [
            ViewPatients, ViewAppointments, EditAppointments,
            RescheduleAppointments, ViewMedicalRecords, UpdateVitals
        ];
    }
}