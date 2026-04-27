namespace TelemedApp.Domain.Entities
{
    public class LabResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public Guid TestId { get; set; }
        public DateTime CollectedAt { get; set; }
        public DateTime? ReportedAt { get; set; }
        public string ResultSummary { get; set; } = string.Empty;
        public string RawDataUrl { get; set; } = string.Empty; // optional link to PDF/image
        public string Status { get; set; } = "Pending"; // Pending, Completed, Reviewed
    }
}