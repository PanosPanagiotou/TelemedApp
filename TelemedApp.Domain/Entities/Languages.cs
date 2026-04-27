namespace TelemedApp.Domain.Entities
{
    public class Language
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public string? LanguageName { get; set; }
    }
}