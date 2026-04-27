namespace TelemedApp.Shared.Authorization
{
    public static class Permissions
    {
        public static IEnumerable<string> GetAll() =>
            PermissionGroupBase.Combine(
                Doctors.All(),
                Nurses.All(),
                Patients.All(),
                Pharmacists.All(),
                Billing.All(),
                Support.All(),
                Telemedicine.All(),
                Audit.All(),
                Lab.All(),
                Maintenance.All(),
                AdminPermissions.All()
            );
    }
}