using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TelemedApp.UI.Client.Shared.Components.Metrics
{
    public partial class MetricCard : ComponentBase
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;

        [Parameter] public MetricCardModel? Model { get; set; }

        // Direct parameters
        [Parameter] public string? Title { get; set; }
        [Parameter] public string? Value { get; set; }
        [Parameter] public string? Subtitle { get; set; }
        [Parameter] public string? IconCss { get; set; }
        [Parameter] public string? Trend { get; set; } // "up" or "down"
        [Parameter] public string? Icon { get; set; }

        // NEW
        [Parameter] public bool Compact { get; set; } = false;
        [Parameter] public bool Clickable { get; set; } = false;
        [Parameter] public EventCallback OnClick { get; set; }

        private ElementReference valueRef;
        private ElementReference cardRef;

        private async Task HandleClick()
        {
            if (Clickable)
                await OnClick.InvokeAsync();
        }

        // Effective values
        private string EffectiveTitle => Model?.Title ?? Title ?? "";
        private string EffectiveValue => Model?.Value ?? Value ?? "";
        private string EffectiveSubtitle => Model?.Subtitle ?? Subtitle ?? "";
        private string EffectiveIcon => Model?.IconCss ?? Icon ?? IconCss ?? "";
        private string EffectiveTrend => Model?.Trend ?? Trend ?? "";

        // Trend UI
        private RenderFragment? TrendIcon => EffectiveTrend switch
        {
            "up" => BuildIcon("fa-solid fa-arrow-up text-green-500"),
            "down" => BuildIcon("fa-solid fa-arrow-down text-red-500"),
            _ => null
        };

        private string TrendLabel => EffectiveTrend switch
        {
            "up" => "Increasing",
            "down" => "Decreasing",
            _ => ""
        };


        // Classes
        private string WrapperClasses =>
            "flex flex-col rounded-xl p-6 transition-colors duration-200 " +
            (Clickable ? "cursor-pointer hover:shadow-md" : "") +
            " bg-white text-gray-900 dark:bg-gray-900 dark:text-gray-100 shadow-sm";

        private string TitleClasses =>
            Compact
                ? "text-xs font-medium text-gray-600 dark:text-gray-300"
                : "text-sm font-medium text-gray-600 dark:text-gray-300";

        private string ValueClasses =>
            Compact
                ? "text-2xl font-bold"
                : "text-3xl font-bold";

        private string SubtitleClasses =>
            Compact
                ? "mt-1 text-[11px] text-gray-500 dark:text-gray-400"
                : "mt-1 text-xs text-gray-500 dark:text-gray-400";

        private string TrendClasses =>
            Compact
                ? "mt-1 flex items-center gap-1 text-[11px]"
                : "mt-1 flex items-center gap-1 text-xs";

        private RenderFragment BuildIcon(string css) => builder =>
        {
            builder.OpenElement(0, "i");
            builder.AddAttribute(1, "class", css);
            builder.CloseElement();
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && int.TryParse(EffectiveValue, out int target))
            {
                await JS.InvokeVoidAsync("animateCounter", valueRef, target);
            }
        }
    }
}