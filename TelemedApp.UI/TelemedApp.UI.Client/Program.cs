using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TelemedApp.UI.Client;
using TelemedApp.UI.Client.Auth;
using TelemedApp.UI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load appsettings.json
using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<Dictionary<string, string>>("appsettings.json");
var apiBaseUrl = config?["ApiBaseUrl"] ?? throw new Exception("ApiBaseUrl missing");

// Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<PermissionGuard>();
builder.Services.AddScoped<FeatureGuard>();
builder.Services.AddScoped<RoleGuard>();


// Named HttpClient (API)
builder.Services.AddHttpClient("TelemedApp.API", async (sp, client) =>
{
    var js = sp.GetRequiredService<IJSRuntime>();

    client.BaseAddress = new Uri(apiBaseUrl);

    var token = await js.InvokeAsync<string?>("localStorage.getItem", "authToken");

    if (!string.IsNullOrWhiteSpace(token))
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
});

// Default HttpClient
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("TelemedApp.API"));

// App services
builder.Services.AddSingleton<ToastService>();
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<PatientsService>();
builder.Services.AddScoped<DoctorsService>();
builder.Services.AddScoped<AppointmentsService>();

await builder.Build().RunAsync();