using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using TelemedApp.UI.Client.Auth;
using TelemedApp.UI.Client.Models;

namespace TelemedApp.UI.Client.Services
{
    public class ApiClient(HttpClient http, IJSRuntime js, AuthService authService, CustomAuthStateProvider authStateProvider)
    {
        private readonly HttpClient _http = http;
        private readonly IJSRuntime _js = js;
        private readonly AuthService _authService = authService;
        private readonly CustomAuthStateProvider _authStateProvider = authStateProvider;

        private async Task AddAuthHeader()
        {
            var token = await _js.InvokeAsync<string?>("sessionStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                _http.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<bool> TryRefreshToken()
        {
            var token = await _js.InvokeAsync<string?>("sessionStorage.getItem", "authToken");
            var refresh = await _js.InvokeAsync<string?>("sessionStorage.getItem", "refreshToken");

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(refresh))
                return false;

            var result = await _authService.RefreshToken(token, refresh);

            if (result == null)
            {
                // Refresh failed → logout
                await _authStateProvider.Logout();
                return false;
            }

            // Refresh succeeded → store new tokens
            await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", result.Token!);
            await _js.InvokeVoidAsync("sessionStorage.setItem", "refreshToken", result.RefreshToken!);

            // Update auth state
            await _authStateProvider.SetToken(result.Token!);
            var user = _authService.ParseToken(result.Token!);
            _authStateProvider.SetUser(user);

            return true;
        }

        private async Task<HttpResponseMessage> SendWithRefresh(Func<Task<HttpResponseMessage>> send)
        {
            await AddAuthHeader();
            var response = await send();

            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return response;

            // Try refresh
            var refreshed = await TryRefreshToken();
            if (!refreshed)
                return response;

            // Retry request
            await AddAuthHeader();
            return await send();
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var res = await SendWithRefresh(() => _http.GetAsync(url));
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> PostAsync<T>(string url, object body)
        {
            var res = await SendWithRefresh(() => _http.PostAsJsonAsync(url, body));
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<T>();
        }

        public async Task PutAsync(string url, object body)
        {
            var res = await SendWithRefresh(() => _http.PutAsJsonAsync(url, body));
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            var res = await SendWithRefresh(() => _http.DeleteAsync(url));
            res.EnsureSuccessStatusCode();
        }
    }
}
