using Microsoft.AspNetCore.Components;

namespace TelemedApp.UI.Client.Shared.Components.Tabs
{
    public class TabItemBase : ComponentBase
    {
        [CascadingParameter] public TabsContainer? Parent { get; set; }
        [CascadingParameter] public bool IsDark { get; set; }

        [Parameter] public string Value { get; set; } = "";
        [Parameter] public RenderFragment? ChildContent { get; set; }

        protected bool Active => Parent?.Value == Value;

        protected async Task Select()
        {
            if (Parent is not null)
                await Parent.SetValue(Value);
        }

        protected string BuildClasses()
        {
            return Parent?.Style switch
            {
                TabsStyle.Underline => UnderlineClasses(),
                TabsStyle.Pill => PillClasses(),
                TabsStyle.Segmented => SegmentedClasses(),
                _ => ""
            };
        }

        private string UnderlineClasses()
        {
            var baseClass = "px-4 py-2 -mb-px border-b-2 transition-colors duration-200";

            return Active
                ? baseClass + " " +
                  (IsDark ? "border-blue-400 text-blue-300" : "border-blue-600 text-blue-700")
                : baseClass + " border-transparent " +
                  (IsDark ? "text-gray-400 hover:text-gray-200" : "text-gray-600 hover:text-gray-900");
        }

        private string PillClasses()
        {
            var baseClass = "px-4 py-1.5 rounded-md text-sm transition-colors duration-200";

            return Active
                ? baseClass + " bg-blue-600 text-white"
                : baseClass + " " +
                  (IsDark ? "text-gray-300 hover:bg-gray-700" : "text-gray-700 hover:bg-gray-200");
        }

        private string SegmentedClasses()
        {
            var baseClass = "px-4 py-1.5 rounded-md text-sm transition-all duration-200";

            return Active
                ? baseClass + " " +
                  (IsDark ? "bg-gray-700 text-white" : "bg-white text-gray-900 shadow-sm")
                : baseClass + " " +
                  (IsDark ? "text-gray-300 hover:bg-gray-800" : "text-gray-700 hover:bg-gray-100");
        }
    }
}