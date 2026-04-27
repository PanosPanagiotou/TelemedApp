using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TelemedApp.UI.Client.Models;

namespace TelemedApp.UI.Client.Services
{
    public class AuthService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        // REGISTER
        public async Task<TokenWrapper> Register(string fullName, string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", new
            {
                FullName = fullName,
                Email = email,
                Password = password
            });

            var wrapper = await response.Content.ReadFromJsonAsync<TokenWrapper>()
                          ?? new TokenWrapper { Success = false };

            return wrapper;
        }

        // LOGIN
        public async Task<TokenWrapper> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new
            {
                Email = email,
                Password = password
            });

            var wrapper = await response.Content.ReadFromJsonAsync<TokenWrapper>()
                          ?? new TokenWrapper { Success = false };

            return wrapper;
        }

        // REFRESH TOKEN
        public async Task<RefreshResponse?> RefreshToken(string token, string refreshToken)
        {
            var response = await _http.PostAsJsonAsync("api/auth/refresh", new
            {
                Token = token,
                RefreshToken = refreshToken
            });

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<RefreshResponse>();
            return result;
        }

        public AuthUser ParseToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var user = new AuthUser
            {
                UserId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value,
                Email = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value,
                FullName = token.Claims.FirstOrDefault(c => c.Type == "fullName")?.Value,

                Roles =
                [
                    .. token.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                ],

                Permissions =
                [
                    .. token.Claims
                        .Where(c => c.Type == "permission")
                        .Select(c => c.Value)
                ],

                Features =
                [
                    .. token.Claims
                        .Where(c => c.Type == "feature")
                        .Select(c => c.Value)
                ]
            };

            return user;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<UserViewModel>>("api/admin/users");
            return result ?? [];
        }

        public async Task AssignRole(string userId, string role)
        {
            var payload = new { userId, role };
            await _http.PostAsJsonAsync("api/admin/assign-role", payload);
        }

        public async Task<List<RoleViewModel>> GetRoles()
        {
            var result = await _http.GetFromJsonAsync<List<RoleViewModel>>("api/admin/roles");
            return result ?? [];
        }

        public async Task<List<string>> GetAllPermissions()
        {
            var result = await _http.GetFromJsonAsync<List<string>>("api/admin/permissions");
            return result ?? [];
        }

        public async Task UpdateRolePermissions(string role, List<string> permissions)
        {
            var payload = new { role, permissions };
            await _http.PostAsJsonAsync("api/admin/roles/update-permissions", payload);
        }

        public async Task<List<FeatureFlagViewModel>> GetFeatureFlags()
        {
            return await _http.GetFromJsonAsync<List<FeatureFlagViewModel>>("api/admin/features")
                   ?? [];
        }

        public async Task UpdateFeatureFlags(List<FeatureFlagViewModel> flags)
        {
            await _http.PostAsJsonAsync("api/admin/features/update", flags);
        }

    }
}