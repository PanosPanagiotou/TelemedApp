namespace TelemedApp.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid? HeadId { get; set; } // HeadOfDepartment (may be a Doctor)
        public string Location { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}