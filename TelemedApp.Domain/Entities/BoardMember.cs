namespace TelemedApp.Domain.Entities
{
    public class BoardMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty; // e.g., Chair, Member
        public bool IsExternal { get; set; } = true; // external investor or internal doctor
        public string Contact { get; set; } = string.Empty;
    }
}