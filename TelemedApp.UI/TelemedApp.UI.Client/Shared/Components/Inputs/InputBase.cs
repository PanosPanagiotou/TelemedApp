using Microsoft.AspNetCore.Components;

namespace TelemedApp.UI.Client.Shared.Components.Inputs
{
    public class InputBase : ComponentBase
    {
        // VARIANTS: default, danger, success, warning, subtle
        [Parameter] public string Variant { get; set; } = "default";

        // SIZES: sm, md, lg
        [Parameter] public string Size { get; set; } = "md";

        // ROUNDED: md, lg, full
        [Parameter] public string Rounded { get; set; } = "lg";

        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Error { get; set; }
        [Parameter] public bool FullWidth { get; set; } = true;

        [CascadingParameter] public bool IsDark { get; set; }

        protected string BuildClasses()
        {
            var baseClass =
                "transition-all duration-200 focus:outline-none " +
                "focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed";

            var sizeClass = Size switch
            {
                "sm" => "px-2 py-1 text-sm",
                "lg" => "px-4 py-3 text-base",
                _ => "px-3 py-2 text-sm"
            };

            var roundedClass = Rounded switch
            {
                "md" => "rounded-md",
                "full" => "rounded-full",
                _ => "rounded-lg"
            };

            var variantClass = Variant switch
            {
                "danger" => Error
                    ? "border-red-500 focus:ring-red-400"
                    : "border-red-400 focus:ring-red-300",

                "success" => "border-green-500 focus:ring-green-400",
                "warning" => "border-yellow-500 focus:ring-yellow-400",
                "subtle" => IsDark
                    ? "border-gray-700 bg-gray-800 text-gray-100"
                    : "border-gray-300 bg-gray-50 text-gray-900",

                _ => Error
                    ? "border-red-500 focus:ring-red-400"
                    : IsDark
                        ? "border-gray-700 bg-gray-800 text-gray-100"
                        : "border-gray-300 bg-white text-gray-900"
            };

            var widthClass = FullWidth ? "w-full" : "";

            return $"{baseClass} {sizeClass} {roundedClass} {variantClass} {widthClass}";
        }
    }
}
