using TelemedApp.UI.Client.Shared.Components.Toast;

namespace TelemedApp.UI.Client.Services
{
    public class ToastService
    {
        public event Action<ToastMessage>? OnShow;

        public void ShowSuccess(string message, int durationMs = 4000) =>
            Show(message, ToastType.Success, durationMs);

        public void ShowError(string message, int durationMs = 4000) =>
            Show(message, ToastType.Error, durationMs);

        public void ShowInfo(string message, int durationMs = 4000) =>
            Show(message, ToastType.Info, durationMs);

        public void ShowWarning(string message, int durationMs = 4000) =>
            Show(message, ToastType.Warning, durationMs);

        public void Show(string message, ToastType type, int durationMs = 4000)
        {
            OnShow?.Invoke(new ToastMessage
            {
                Message = message,
                Type = type,
                DurationMs = durationMs,
                Timestamp = DateTime.Now
            });
        }

        public void Show(string message, ToastType type, string actionLabel, Action action, int durationMs = 4000)
        {
            OnShow?.Invoke(new ToastMessage
            {
                Message = message,
                Type = type,
                DurationMs = durationMs,
                ActionLabel = actionLabel,
                Action = action,
                Timestamp = DateTime.Now
            });
        }

        public void Show(string message, ToastType type, string iconOverride, int durationMs = 4000)
        {
            OnShow?.Invoke(new ToastMessage
            {
                Message = message,
                Type = type,
                DurationMs = durationMs,
                Icon = iconOverride,
                Timestamp = DateTime.Now
            });
        }
    }
}