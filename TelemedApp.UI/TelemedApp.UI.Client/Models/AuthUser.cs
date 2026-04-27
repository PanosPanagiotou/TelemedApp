namespace TelemedApp.UI.Client.Models
{
    public class AuthUser
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public List<string> Roles { get; set; } = [];
        public List<string> Permissions { get; set; } = [];
        public List<string> Features { get; set; } = [];

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(UserId);
    }

}
