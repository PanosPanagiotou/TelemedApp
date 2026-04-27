namespace TelemedApp.Shared.Authorization
{
    public static class Patients
    {
        public const string ViewSelf = "Patients.ViewSelf";
        public const string EditSelf = "Patients.EditSelf";
        public const string BookAppointment = "Patients.BookAppointment";
        public const string ViewPrescriptions = "Patients.ViewPrescriptions";
        public const string ViewMedicalRecords = "Patients.ViewMedicalRecords";

        public static IEnumerable<string> All() =>
        [
            ViewSelf, EditSelf, BookAppointment,
            ViewPrescriptions, ViewMedicalRecords
        ];
    }
}