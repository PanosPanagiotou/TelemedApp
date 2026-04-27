namespace TelemedApp.UI.Client.Shared.Components.Toast
{
    public class ToastMessage
    {
        public string Message { get; set; } = "";
        public ToastType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public int DurationMs { get; set; } = 4000;
        public string? ActionLabel { get; set; }
        public Action? Action { get; set; }
        public string? Icon { get; set; }
    }
}