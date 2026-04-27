using System.Collections.Generic;

namespace TelemedApp.Shared.Features
{
    public static class FeatureCatalog
    {
        public static readonly List<FeatureFlagDto> All =
        [
            new FeatureFlagDto { Name = "NewDashboard", Enabled = true },
            new FeatureFlagDto { Name = "AdvancedCalendar", Enabled = false },
            new FeatureFlagDto { Name = "AIInsights", Enabled = false },
            new FeatureFlagDto { Name = "LabV2", Enabled = false },
            new FeatureFlagDto { Name = "BillingV2", Enabled = false }
        ];

        public static void Update(List<FeatureFlagDto> flags)
        {
            foreach (var f in flags)
            {
                var existing = All.FirstOrDefault(x => x.Name == f.Name);
                if (existing != null)
                    existing.Enabled = f.Enabled;
            }
        }
    }
}