using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelemedApp.Identity.Interfaces;
using TelemedApp.Identity.Models;

namespace TelemedApp.Identity.Services
{
    public class IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService) : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<AuthResult> RegisterAsync(string email, string password, string fullName)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FullName = fullName,
                PasswordLastChangedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            // Default role
            await _userManager.AddToRoleAsync(user, "User");

            var token = await _tokenService.GenerateJwtToken(user);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = token.RefreshToken
            };
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = ["Invalid credentials"]
                };
            }

            if (!user.EmailConfirmed)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = ["Email not confirmed"]
                };
            }

            if (!user.IsActive)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = [ "Account disabled" ]
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = ["Account is locked. Please contact administrator."]
                };
            }

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = ["Invalid credentials"]
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var expiryMonths = role switch
            {
                "Admin" => 2,
                "Doctor" => 3,
                "Nurse" => 4,
                "User" => 5,
                "SupportStaff" => 6,
                _ => 6
            };

            if (user.PasswordLastChangedAt.AddMonths(expiryMonths) < DateTime.UtcNow)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = ["Password expired. Please reset your password."]
                };
            }

            var token = await _tokenService.GenerateJwtToken(user);

            return new AuthResult
            {
                Success = true,
                Token = token.Token,
                RefreshToken = token.RefreshToken
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var result = await _tokenService.RefreshTokenAsync(token, refreshToken);

            if (!result.Success)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = result.Errors
                };
            }

            return new AuthResult
            {
                Success = true,
                Token = result.Token,
                RefreshToken = result.RefreshToken
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var list = new List<UserDto>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);

                list.Add(new UserDto
                {
                    Id = u.Id,
                    Email = u.Email ?? string.Empty,
                    FullName = u.FullName,
                    Roles = roles
                });
            }

            return list;
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new ApplicationRole { Name = role });

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<IEnumerable<RolePermissionsDto>> GetAllRolesWithPermissionsAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var list = new List<RolePermissionsDto>();

            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);

                var permissions = claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value)
                    .ToList();

                list.Add(new RolePermissionsDto
                {
                    Role = role.Name!,
                    Permissions = permissions
                });
            }

            return list;
        }

        public async Task<bool> UpdateRolePermissionsAsync(string roleName, List<string> permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            var existingClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in existingClaims.Where(c => c.Type == "permission"))
                await _roleManager.RemoveClaimAsync(role, claim);

            foreach (var perm in permissions)
                await _roleManager.AddClaimAsync(role, new Claim("permission", perm));

            return true;
        }
    }
}