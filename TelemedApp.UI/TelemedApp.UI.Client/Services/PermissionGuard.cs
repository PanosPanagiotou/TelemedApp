using TelemedApp.UI.Client.Models;

namespace TelemedApp.UI.Client.Services
{
    public class PermissionGuard(AuthState state)
    {
        private readonly AuthState _state = state;

        public bool Has(string permission)
            => _state.User.Permissions.Contains(permission);
    }
}