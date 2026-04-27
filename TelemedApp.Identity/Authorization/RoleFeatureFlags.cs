namespace TelemedApp.Identity.Authorization
{
    public static class RoleFeatureFlags
    {
        public static readonly Dictionary<string, HashSet<string>> Map = new()
        {
            // Full access to all features
            ["Admin"] =
            [
                FeatureFlags.NewDashboard,
                FeatureFlags.AdvancedCalendar,
                FeatureFlags.AIInsights,
                FeatureFlags.LabV2,
                FeatureFlags.BillingV2
            ],

            // Doctors get clinical features
            ["Doctor"] =
            [
                FeatureFlags.NewDashboard,
                FeatureFlags.AdvancedCalendar,
                FeatureFlags.LabV2
            ],

            // Patients get minimal features
            ["Patient"] =
            [
                FeatureFlags.NewDashboard
            ],

            // Support staff features
            ["Support"] =
            [
                FeatureFlags.NewDashboard,
                FeatureFlags.AdvancedCalendar
            ],

            // Lab technicians
            ["Lab"] =
            [
                FeatureFlags.LabV2
            ],

            // Billing department
            ["Billing"] =
            [
                FeatureFlags.BillingV2
            ],

            // Telemedicine operators
            ["Telemedicine"] =
            [
                FeatureFlags.NewDashboard,
                FeatureFlags.AdvancedCalendar
            ],

            // Maintenance staff
            ["Maintenance"] = [],

            // Audit / Security
            ["Audit"] =
            [
                FeatureFlags.NewDashboard
            ]
        };
    }
}