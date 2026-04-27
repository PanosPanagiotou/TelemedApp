namespace TelemedApp.Domain.Entities
{
    public class Nurse : StaffBase
    {
        public string Certification { get; set; } = string.Empty;
        public Guid? DepartmentId { get; set; }
    }
}