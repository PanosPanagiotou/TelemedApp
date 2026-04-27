using Microsoft.AspNetCore.Components;

namespace TelemedApp.UI.Client.Models
{
    public class ModalStep
    {
        public string Title { get; set; } = "";
        public RenderFragment? Content { get; set; }
    }

}
