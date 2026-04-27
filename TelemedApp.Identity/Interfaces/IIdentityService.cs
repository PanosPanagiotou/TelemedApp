using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthResult> RegisterAsync(string email, string password, string fullName);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshTokenAsync(string token, string refreshToken);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> AssignRoleAsync(string userId, string role);
        Task<IEnumerable<RolePermissionsDto>> GetAllRolesWithPermissionsAsync();
        Task<bool> UpdateRolePermissionsAsync(string role, List<string> permissions);
    }
}