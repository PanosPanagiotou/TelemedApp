namespace TelemedApp.UI.Client.Shared.Components.Metrics
{
    public class MetricCardModel
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Subtitle { get; set; }

        // Legacy / backward compatible
        public string? IconCss { get; set; }

        // New refined API
        public string? Icon { get; set; }
        public string? Trend { get; set; }

        public string? CssClass { get; set; }
    }
}