using Microsoft.AspNetCore.Components;

namespace TelemedApp.UI.Client.Shared.Components.Buttons
{
    public partial class Button : ComponentBase
    {
        // VARIANTS
        [Parameter] public string Variant { get; set; } = "primary";

        // TONES: solid, soft, subtle
        [Parameter] public string Tone { get; set; } = "solid";

        // SIZES: sm, md, lg
        [Parameter] public string Size { get; set; } = "md";

        // ROUNDED: md, lg, full
        [Parameter] public string Rounded { get; set; } = "lg";
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public bool FullWidth { get; set; }
        [Parameter] public string Type { get; set; } = "button";
        [Parameter] public bool Shadow { get; set; } = false;
        [Parameter] public bool Gradient { get; set; } = false;
        [Parameter] public bool HoverEffect { get; set; } = true;
        [Parameter] public bool ActiveScale { get; set; } = true;
        [Parameter] public bool LoadingOverlay { get; set; } = true;
        [Parameter] public string IconPosition { get; set; } = "left";
        [Parameter] public RenderFragment? Icon { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string? CssClass { get; set; }

        private async Task HandleClick()
        {
            if (!Disabled && !Loading)
                await OnClick.InvokeAsync();
        }

        private string ContentClasses =>
            Loading && LoadingOverlay ? "opacity-0" : "opacity-100";

        private Dictionary<string, object> GetDisabledAttribute()
        {
            if (Disabled || Loading)
                return new() { { "disabled", "disabled" } };

            return [];
        }


        private string BuildClasses()
        {
            var baseClass =
                "relative inline-flex items-center justify-center font-medium transition-all " +
                "focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed";

            var sizeClass = Size switch
            {
                "sm" => "px-3 py-1.5 text-sm",
                "lg" => "px-5 py-3 text-base",
                _ => "px-4 py-2 text-sm"
            };

            var roundedClass = Rounded switch
            {
                "md" => "rounded-md",
                "full" => "rounded-full",
                _ => "rounded-lg"
            };

            var toneClass = GetToneClass();
            var shadowClass = Shadow ? "shadow-sm dark:shadow-gray-900/40" : "";
            var gradientClass = Gradient ? "bg-gradient-to-r from-purple-500 to-blue-500 text-white" : "";
            var hoverClass = HoverEffect ? "hover:brightness-110" : "";
            var activeClass = ActiveScale ? "active:scale-[0.97]" : "";
            var widthClass = FullWidth ? "w-full" : "";

            return $"{baseClass} {sizeClass} {roundedClass} {toneClass} {shadowClass} {gradientClass} {hoverClass} {activeClass} {widthClass} {CssClass}".Trim();
        }

        private string GetToneClass()
        {
            return (Variant, Tone) switch
            {
                // PRIMARY
                ("primary", "solid") => "bg-blue-600 text-white hover:bg-blue-700 focus:ring-blue-500",
                ("primary", "soft") => "bg-blue-100 text-blue-800 dark:bg-blue-800 dark:text-blue-100",
                ("primary", "subtle") => "bg-blue-50 text-blue-700 dark:bg-blue-900 dark:text-blue-200",

                // SECONDARY
                ("secondary", "solid") => "bg-gray-200 text-gray-800 hover:bg-gray-300 dark:bg-gray-700 dark:text-gray-200 dark:hover:bg-gray-600",
                ("secondary", "soft") => "bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-200",
                ("secondary", "subtle") => "bg-gray-50 text-gray-700 dark:bg-gray-900 dark:text-gray-300",

                // ACCENT
                ("accent", "solid") => "bg-purple-600 text-white hover:bg-purple-700 focus:ring-purple-500",
                ("accent", "soft") => "bg-purple-100 text-purple-800 dark:bg-purple-800 dark:text-purple-100",
                ("accent", "subtle") => "bg-purple-50 text-purple-700 dark:bg-purple-900 dark:text-purple-200",

                // DANGER
                ("danger", "solid") => "bg-red-600 text-white hover:bg-red-700 focus:ring-red-500",
                ("danger", "soft") => "bg-red-100 text-red-800 dark:bg-red-800 dark:text-red-100",
                ("danger", "subtle") => "bg-red-50 text-red-700 dark:bg-red-900 dark:text-red-200",

                // OUTLINE
                ("outline", _) => "border border-gray-300 text-gray-800 hover:bg-gray-100 dark:border-gray-600 dark:text-gray-200 dark:hover:bg-gray-700",

                // GHOST
                ("ghost", _) => "text-gray-700 hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700",

                _ => "bg-blue-600 text-white"
            };
        }
    }
}