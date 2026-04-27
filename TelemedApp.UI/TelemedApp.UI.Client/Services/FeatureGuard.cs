namespace TelemedApp.UI.Client.Services
{
    public class FeatureGuard(AuthState state)
    {
        private readonly AuthState _state = state;

        public bool Has(string feature)
            => _state.User.Features.Contains(feature);
    }
}