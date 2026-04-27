using Microsoft.AspNetCore.Components;

namespace TelemedApp.UI.Client.Shared.Components.Avatars
{
    public partial class Avatar : ComponentBase
    {
        [CascadingParameter] public bool IsDark { get; set; }

        [Parameter] public string? Src { get; set; }
        [Parameter] public string Alt { get; set; } = "";
        [Parameter] public string? Initials { get; set; }
        [Parameter] public AvatarSize Size { get; set; } = AvatarSize.Md;
        [Parameter] public AvatarShape Shape { get; set; } = AvatarShape.Circle;

        [Parameter] public bool ShowStatus { get; set; }
        [Parameter] public AvatarStatus Status { get; set; } = AvatarStatus.Offline;

        [Parameter] public bool WithRing { get; set; } = false;
        [Parameter] public bool HoverEffect { get; set; } = false;
        [Parameter] public bool Bordered { get; set; } = false;
        [Parameter] public bool Square { get; set; } = false;

        [Parameter] public bool GradientRing { get; set; } = false;
        [Parameter] public bool SoftShadow { get; set; } = false;
        [Parameter] public bool AutoColorFallback { get; set; } = true;

        [Parameter] public RenderFragment? FallbackContent { get; set; }

        private bool ImageLoaded { get; set; }

        private void OnImageLoaded() => ImageLoaded = true;

        private string Tooltip =>
            ShowStatus ? Status switch
            {
                AvatarStatus.Online => "Available",
                AvatarStatus.Busy => "Do not disturb",
                AvatarStatus.Offline => "Offline",
                _ => "Status"
            } : Alt;

        private string WrapperClasses =>
            $"{SizeClass} relative inline-flex items-center justify-center overflow-hidden " +
            $"{ShapeClass} {RingClass} {BorderClass} {HoverClass} {ShadowClass} transition-all duration-200";

        private string ImageClasses =>
            $"w-full h-full object-cover {(ImageLoaded ? "opacity-100" : "opacity-0")} transition-opacity duration-300";

        private string FallbackClasses =>
            $"w-full h-full flex items-center justify-center font-semibold select-none {FallbackColor}";

        private string StatusClasses =>
            $"absolute bottom-0 right-0 block rounded-full ring-2 ring-white {StatusColor} {StatusSize} {PulseClass}";

        private string SizeClass => Size switch
        {
            AvatarSize.Sm => "w-8 h-8 text-xs",
            AvatarSize.Md => "w-10 h-10 text-sm",
            AvatarSize.Lg => "w-14 h-14 text-base",
            _ => "w-10 h-10"
        };

        private string ShapeClass =>
            Square ? "rounded-none" :
            Shape == AvatarShape.Circle ? "rounded-full" : "rounded-lg";

        private string RingClass =>
            GradientRing
                ? "ring-2 ring-transparent bg-gradient-to-br from-blue-500 to-purple-500 p-[2px]"
                : WithRing
                    ? (IsDark ? "ring-1 ring-gray-700" : "ring-1 ring-gray-300")
                    : "";

        private string BorderClass =>
            Bordered
                ? (IsDark ? "border border-gray-600" : "border border-gray-300")
                : "";

        private string HoverClass =>
            HoverEffect ? "hover:brightness-110" : "";

        private string ShadowClass =>
            SoftShadow ? "shadow-sm dark:shadow-gray-900/40" : "";

        private string StatusSize => Size switch
        {
            AvatarSize.Sm => "w-2.5 h-2.5",
            AvatarSize.Md => "w-3 h-3",
            AvatarSize.Lg => "w-3.5 h-3.5",
            _ => "w-3 h-3"
        };

        private string StatusColor => Status switch
        {
            AvatarStatus.Online => "bg-green-500",
            AvatarStatus.Busy => "bg-yellow-500",
            AvatarStatus.Offline => IsDark ? "bg-gray-600" : "bg-gray-400",
            _ => "bg-gray-400"
        };

        private string PulseClass =>
            Status == AvatarStatus.Online ? "animate-pulse" : "";

        private string FallbackColor =>
            AutoColorFallback && !string.IsNullOrWhiteSpace(Initials)
                ? GetColorFromName(Initials)
                : (IsDark ? "bg-gray-700 text-gray-200" : "bg-gray-200 text-gray-700");

        private string GetColorFromName(string name)
        {
            int hash = name.GetHashCode();
            int colorIndex = Math.Abs(hash % 6);

            return colorIndex switch
            {
                0 => "bg-blue-500 text-white",
                1 => "bg-purple-500 text-white",
                2 => "bg-green-500 text-white",
                3 => "bg-pink-500 text-white",
                4 => "bg-indigo-500 text-white",
                _ => "bg-gray-500 text-white"
            };
        }
    }
}
