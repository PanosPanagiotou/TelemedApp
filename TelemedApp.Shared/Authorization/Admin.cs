namespace TelemedApp.Shared.Authorization
{
    public static class AdminPermissions
    {
        public const string Access = "Admin.Access"; // Access to admin endpoints
        public const string ManageUsers = "Admin.ManageUsers"; // View/manage users
        public const string AssignRoles = "Admin.AssignRoles"; // Assign roles to users

        public static IEnumerable<string> All() =>
        [
            Access,
            ManageUsers,
            AssignRoles
        ];
    }
}