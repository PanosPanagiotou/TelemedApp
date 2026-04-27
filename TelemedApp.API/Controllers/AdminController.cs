using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelemedApp.API.Models;
using TelemedApp.Application.Requests.Admin;
using TelemedApp.Identity.Interfaces;
using TelemedApp.Identity.Models;
using TelemedApp.Shared.Authorization;
using TelemedApp.Shared.Features;

namespace TelemedApp.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Policy = "Admin.Access")]
    public class AdminController(IIdentityService identity) : ControllerBase
    {
        private readonly IIdentityService _identity = identity;

        [HttpGet("users")]
        [Authorize(Policy = "Admin.Users.Manage")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _identity.GetAllUsersAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.Ok(users));
        }

        [HttpPost("assign-role")]
        [Authorize(Policy = "Admin.Roles.Manage")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest req)
        {
            await _identity.AssignRoleAsync(req.UserId, req.Role);
            return Ok(ApiResponse<object?>.Ok(null, "Role assigned successfully"));
        }

        // GET /api/admin/roles
        [HttpGet("roles")]
        [Authorize(Policy = "Admin.Roles.Manage")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _identity.GetAllRolesWithPermissionsAsync();
            return Ok(ApiResponse<IEnumerable<RolePermissionsDto>>.Ok(roles));
        }

        // GET /api/admin/permissions
        [HttpGet("permissions")]
        [Authorize(Policy = "Admin.Roles.Manage")]
        public IActionResult GetPermissions()
        {
            var permissions = PermissionCatalog.AllPermissions;
            return Ok(ApiResponse<IEnumerable<string>>.Ok(permissions));
        }

        // POST /api/admin/roles/update-permissions
        [HttpPost("roles/update-permissions")]
        [Authorize(Policy = "Admin.Roles.Manage")]
        public async Task<IActionResult> UpdateRolePermissions([FromBody] UpdateRolePermissionsRequest req)
        {
            await _identity.UpdateRolePermissionsAsync(req.Role, req.Permissions);
            return Ok(ApiResponse<object?>.Ok(null, "Permissions updated successfully"));
        }

        [HttpGet("features")]
        public IActionResult GetFeatures()
        {
            return Ok(ApiResponse<IEnumerable<FeatureFlagDto>>.Ok(FeatureCatalog.All));
        }

        [HttpPost("features/update")]
        public IActionResult UpdateFeatures([FromBody] List<FeatureFlagDto> flags)
        {
            FeatureCatalog.Update(flags);
            return Ok(ApiResponse<object?>.Ok(null, "Feature flags updated"));
        }

    }
}