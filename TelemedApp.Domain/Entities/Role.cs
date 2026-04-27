namespace TelemedApp.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // Admin, Doctor, Nurse, FrontDesk, Finance
        public List<Permission> Permissions { get; set; } = [];
    }
}