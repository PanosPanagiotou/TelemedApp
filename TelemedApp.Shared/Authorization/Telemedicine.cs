namespace TelemedApp.Shared.Authorization
{
    public static class Telemedicine
    {
        public const string View = "Telemedicine.View";
        public const string Start = "Telemedicine.Start";
        public const string End = "Telemedicine.End";
        public const string Manage = "Telemedicine.Manage";

        public static IEnumerable<string> All() =>
        [
            View, Start, End, Manage
        ];
    }
}