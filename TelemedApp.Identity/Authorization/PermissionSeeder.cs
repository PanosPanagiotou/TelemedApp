using Microsoft.AspNetCore.Identity;
using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Authorization
{
    public static class PermissionSeeder
    {
        public static async Task SeedAsync(
            RoleManager<ApplicationRole> roleManager)
        {
            foreach (var role in RolePermissions.Map)
            {
                var roleName = role.Key;

                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }
        }
    }
}