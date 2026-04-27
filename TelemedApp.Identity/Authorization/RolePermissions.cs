using TelemedApp.Shared.Authorization;

namespace TelemedApp.Identity.Authorization
{
    public static class RolePermissions
    {
        // Maps each role to the permissions it should have.
        // This dictionary is used by the IdentitySeeder during initialization.
        public static readonly Dictionary<string, IEnumerable<string>> Map = new()
        {
            // ---------------------------------------------------------
            // ADMIN — Full system access
            // ---------------------------------------------------------
            ["Admin"] = PermissionGroupBase.Combine(
                Permissions.GetAll(),
                AdminPermissions.All()
            ),

            // ---------------------------------------------------------
            // DOCTOR — Medical operations + patient access
            // ---------------------------------------------------------
            ["Doctor"] = PermissionGroupBase.Combine(
                Doctors.All(),
                Patients.All()
            ),

            // ---------------------------------------------------------
            // NURSE — Patient operations + vitals + appointments
            // ---------------------------------------------------------
            ["Nurse"] = PermissionGroupBase.Combine(
                Nurses.All(),
                Patients.All()
            ),

            // ---------------------------------------------------------
            // BILLING / FINANCE — Financial operations only
            // ---------------------------------------------------------
            ["Finance"] = Billing.All(),
            ["BillingManager"] = Billing.All(),

            // ---------------------------------------------------------
            // LAB TECHNICIAN — Lab operations
            // ---------------------------------------------------------
            ["LabTechnician"] = Lab.All(),

            // ---------------------------------------------------------
            // MAINTENANCE STAFF — Maintenance operations
            // ---------------------------------------------------------
            ["MaintenanceStaff"] = Maintenance.All(),

            // ---------------------------------------------------------
            // BASIC USER — Self-service permissions only
            // ---------------------------------------------------------
            ["User"] =
            [
                Patients.ViewSelf,
                Patients.EditSelf
            ]
        };
    }
}
