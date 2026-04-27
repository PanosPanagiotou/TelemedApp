using Microsoft.AspNetCore.Authorization;

namespace TelemedApp.API.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string permission)
        {
            Policy = permission;
        }
    }
}