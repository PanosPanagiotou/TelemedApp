using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TelemedApp.Identity.Models;
using TelemedApp.Shared.Authorization;

namespace TelemedApp.Identity.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("IdentitySeeder");
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            logger.LogInformation("🔐 Starting Identity seeding...");

            // ---------------------------------------------------------
            // 1. Create roles
            // ---------------------------------------------------------
            string[] roles =
            [
                "Admin",
                "User",
                "Doctor",
                "Nurse",
                "Finance",
                "BillingManager",
                "LabTechnician",
                "MaintenanceStaff"
            ];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                    logger.LogInformation("✔ Created role: {Role}", role);
                }
            }

            // ---------------------------------------------------------
            // 2. Assign permissions to roles
            // ---------------------------------------------------------
            async Task AddPermissions(string role, IEnumerable<string> permissions)
            {
                var r = await roleManager.FindByNameAsync(role)
                        ?? throw new InvalidOperationException($"Role '{role}' not found.");

                var existing = await roleManager.GetClaimsAsync(r);

                foreach (var permission in permissions)
                {
                    if (!existing.Any(c => c.Type == "permission" && c.Value == permission))
                    {
                        await roleManager.AddClaimAsync(r, new Claim("permission", permission));
                        logger.LogInformation("✔ Added permission '{Permission}' to role '{Role}'", permission, role);
                    }
                }
            }

            // Admin → all permissions
            await AddPermissions("Admin", Permissions.GetAll());

            // Doctor
            await AddPermissions("Doctor", PermissionGroupBase.Combine(
                Doctors.All(),
                Patients.All()
            ));

            // Nurse
            await AddPermissions("Nurse", PermissionGroupBase.Combine(
                Nurses.All(),
                Patients.All()
            ));

            // Finance / Billing
            await AddPermissions("Finance", Billing.All());
            await AddPermissions("BillingManager", Billing.All());

            // Lab Technician
            await AddPermissions("LabTechnician", Lab.All());

            // Maintenance Staff
            await AddPermissions("MaintenanceStaff", Maintenance.All());

            // ---------------------------------------------------------
            // 3. Seed Admin User
            // ---------------------------------------------------------
            var adminEmail = "admin@telemed.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Administrator",
                    EmailConfirmed = true,

                    IsActive = true,
                    PasswordLastChangedAt = DateTime.UtcNow,
                    UserFeatureFlags = []
                };

                var result = await userManager.CreateAsync(admin, "TelemedAdmin123!");
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin: "
                        + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                logger.LogInformation("✔ Admin user created.");
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                logger.LogInformation("✔ Admin assigned to Admin role.");
            }

            logger.LogInformation("🎉 Identity seeding completed successfully.");
        }
    }
}