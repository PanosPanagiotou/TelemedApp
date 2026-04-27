namespace TelemedApp.Application.Requests.Auth
{
    public record RefreshRequest(string Token, string RefreshToken);
}
