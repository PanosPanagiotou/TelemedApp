namespace TelemedApp.Shared.Authorization
{
    public static class Maintenance
    {
        public const string View = "Maintenance.View";         // View maintenance tasks
        public const string Assign = "Maintenance.Assign";     // Assign maintenance tasks
        public const string Complete = "Maintenance.Complete"; // Mark tasks as completed
        public const string Schedule = "Maintenance.Schedule"; // Schedule maintenance tasks

        public static IEnumerable<string> All() =>
        [
            View, Assign, Complete, Schedule
        ];
    }
}