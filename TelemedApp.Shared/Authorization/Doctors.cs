namespace TelemedApp.Shared.Authorization
{
    public static class Doctors
    {
        public const string View = "Doctors.View";
        public const string Create = "Doctors.Create";
        public const string Edit = "Doctors.Edit";
        public const string Delete = "Doctors.Delete";
        public const string Schedule = "Doctors.Schedule";
        public const string Prescribe = "Doctors.Prescribe";
        public const string ViewMedicalRecords = "Doctors.ViewMedicalRecords";
        public const string EditMedicalRecords = "Doctors.EditMedicalRecords";

        public static IEnumerable<string> All() =>
        [
            View, Create, Edit, Delete, Schedule, Prescribe,
            ViewMedicalRecords, EditMedicalRecords
        ];
    }
}