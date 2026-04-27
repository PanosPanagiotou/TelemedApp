namespace TelemedApp.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public Guid? RoomId { get; set; }
    }
}