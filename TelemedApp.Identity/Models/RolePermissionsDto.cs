namespace TelemedApp.Identity.Models
{
    public class RolePermissionsDto
    {
        public string Role { get; set; } = "";
        public List<string> Permissions { get; set; } = [];
    }
}
