namespace TelemedApp.UI.Client.Models
{
    public class LoginResponse
    {
        public TokenWrapper? Token { get; set; }
    }

    public class TokenWrapper
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public List<string>? Errors { get; set; } = [];
    }
}