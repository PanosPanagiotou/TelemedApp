namespace TelemedApp.Domain.Entities
{
    public class StaffBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // e.g., Doctor, Nurse, FrontDesk
        public bool IsActive { get; set; } = true;
    }
}