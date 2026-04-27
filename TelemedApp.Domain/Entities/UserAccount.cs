namespace TelemedApp.Domain.Entities
{
    public class UserAccount
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = [];
        public bool IsActive { get; set; } = true;
    }
}