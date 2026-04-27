namespace TelemedApp.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid? LocationId { get; set; }
        public int Capacity { get; set; } = 1;
        public string Notes { get; set; } = string.Empty;
    }
}