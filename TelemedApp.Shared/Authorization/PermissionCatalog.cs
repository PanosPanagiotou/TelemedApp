namespace TelemedApp.Shared.Authorization
{
    public static class PermissionCatalog
    {
        public static readonly List<string> AllPermissions =
        [
            "Admin.Access",
            "Admin.Users.Manage",
            "Admin.Roles.Manage",

            "Doctors.View",
            "Doctors.Create",
            "Doctors.Edit",
            "Doctors.Delete",

            "Patients.View",
            "Patients.Create",
            "Patients.Edit",
            "Patients.Delete",

            "Appointments.View",
            "Appointments.Create",
            "Appointments.Edit",
            "Appointments.Cancel",

            "LabResults.View",
            "Finance.View",
            "Reports.View"
        ];
    }
}