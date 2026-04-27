namespace TelemedApp.Shared.Authorization
{
    public abstract class PermissionGroupBase
    {
        public static IEnumerable<string> Combine(params IEnumerable<string>[] groups)
        {
            foreach (var group in groups)
            foreach (var permission in group)
                yield return permission;
        }
    }
}