using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using TelemedApp.UI.Client.Models;
using TelemedApp.UI.Client.Services;

namespace TelemedApp.UI.Client.Auth
{
    public class CustomAuthStateProvider(IJSRuntime js, AuthService authService) : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js = js;
        private readonly AuthService _authService = authService;
        private AuthUser? _currentUser;

        public void SetUser(AuthUser user)
        {
            _currentUser = user;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public AuthUser? GetUser() => _currentUser;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string?>("sessionStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                var user = _authService.ParseToken(token);
                _currentUser = user;

                var claims = new List<Claim>();

                // Permissions
                claims.AddRange(user.Permissions.Select(p => new Claim("permission", p)));

                // Roles
                claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

                // Name
                if (!string.IsNullOrWhiteSpace(user.FullName))
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName));

                var identity = new ClaimsIdentity(claims, "jwt");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task SetToken(string token)
        {
            await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "refreshToken");
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
