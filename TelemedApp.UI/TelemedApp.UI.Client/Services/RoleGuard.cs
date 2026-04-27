namespace TelemedApp.UI.Client.Services
{
    public class RoleGuard(AuthState state)
    {
        private readonly AuthState _state = state;

        public bool Is(string role)
            => _state.User.Roles.Contains(role);
    }
}