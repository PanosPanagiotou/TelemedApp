namespace TelemedApp.Shared.Authorization
{
    public static class Billing
    {
        public const string View = "Billing.View";
        public const string Manage = "Billing.Manage";
        public const string GenerateInvoice = "Billing.GenerateInvoice";
        public const string SendInvoice = "Billing.SendInvoice";

        public static IEnumerable<string> All() =>
        [
            View, Manage, GenerateInvoice, SendInvoice
        ];
    }
}