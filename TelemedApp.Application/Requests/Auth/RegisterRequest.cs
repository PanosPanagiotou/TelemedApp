namespace TelemedApp.Application.Requests.Auth
{
    public record RegisterRequest(string Email, string Password, string FullName);
}
