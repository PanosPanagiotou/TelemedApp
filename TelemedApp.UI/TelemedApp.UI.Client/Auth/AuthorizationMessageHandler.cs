using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using TelemedApp.UI.Client.Services;

namespace TelemedApp.UI.Client.Auth
{
    public class AuthorizationMessageHandler(IJSRuntime js, AuthService auth, NavigationManager nav)
    {
        private readonly IJSRuntime _js = js;
        private readonly AuthService _auth = auth;
        private readonly NavigationManager _nav = nav;

        public async Task<HttpRequestMessage> AddAuthorizationAsync(HttpRequestMessage request)
        {
            var token = await _js.InvokeAsync<string?>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return request;
        }
    }
}