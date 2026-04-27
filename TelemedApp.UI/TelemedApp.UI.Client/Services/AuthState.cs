using TelemedApp.UI.Client.Models;

namespace TelemedApp.UI.Client.Services
{
    public class AuthState
    {
        public AuthUser User { get; private set; } = new();

        public void SetUser(AuthUser user)
        {
            User = user;
        }

        public void Clear()
        {
            User = new AuthUser();
        }
    }
}