namespace TelemedApp.Domain.Entities
{
    public class LabTest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TestName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string SpecimenType { get; set; } = string.Empty;
        public TimeSpan TypicalTurnaround { get; set; } = TimeSpan.FromDays(1);
    }
}