namespace TelemedApp.Shared.Authorization
{
    public static class Audit
    {
        public const string View = "Audit.View";
        public const string ViewSecurityEvents = "Audit.ViewSecurityEvents";

        public static IEnumerable<string> All() =>
        [
            View, ViewSecurityEvents
        ];
    }
}