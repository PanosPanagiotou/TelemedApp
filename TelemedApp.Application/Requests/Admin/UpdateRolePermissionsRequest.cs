namespace TelemedApp.Application.Requests.Admin
{
    public class UpdateRolePermissionsRequest
    {
        public string Role { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = [];
    }
}