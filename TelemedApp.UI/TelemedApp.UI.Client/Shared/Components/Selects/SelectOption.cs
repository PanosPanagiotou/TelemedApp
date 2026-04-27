namespace TelemedApp.UI.Client.Shared.Components.Selects
{
    public class SelectOption
    {
        public string Value { get; set; } = "";
        public string Label { get; set; } = "";

        public SelectOption() { }

        public SelectOption(string value, string label)
        {
            Value = value;
            Label = label;
        }
    }
}