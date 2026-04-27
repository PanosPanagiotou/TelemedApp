namespace TelemedApp.Shared.Authorization
{
    public static class Lab
    {
        public const string View = "Lab.View";                 // View lab results
        public const string Approve = "Lab.Approve";           // Approve lab results
        public const string Upload = "Lab.Upload";             // Upload new lab results
        public const string Edit = "Lab.Edit";                 // Edit existing lab results

        public static IEnumerable<string> All() =>
        [
            View, Approve, Upload, Edit
        ];
    }
}