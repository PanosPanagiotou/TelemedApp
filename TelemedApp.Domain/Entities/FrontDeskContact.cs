namespace TelemedApp.Domain.Entities
{
    public class FrontDeskContact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Front Desk";
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Is24x7 { get; set; } = false;
        public string Website { get; set; } = string.Empty;
        public bool HasChatBot { get; set; } = false;
    }
}