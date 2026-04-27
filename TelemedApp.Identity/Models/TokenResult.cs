namespace TelemedApp.Identity.Models
{
    public class TokenResult
    {
        public bool Success { get; set; }
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public IEnumerable<string> Errors { get; set; } = [];
    }
}