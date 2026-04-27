namespace TelemedApp.Shared.Authorization
{
    public static class Pharmacists
    {
        public const string View = "Pharmacists.View";
        public const string Verify = "Pharmacists.Verify";
        public const string Dispense = "Pharmacists.Dispense";

        public static IEnumerable<string> All() =>
        [
            View, Verify, Dispense
        ];
    }
}