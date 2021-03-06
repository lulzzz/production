﻿namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class BuildPlanDetailResource : HypermediaResource
    {
        public string BuildPlanName { get; set; }

        public string PartNumber { get; set; }

        public int FromLinnWeekNumber { get; set; }

        public int? ToLinnWeekNumber { get; set; }

        public string RuleCode { get; set; }

        public int? Quantity { get; set; }
    }
}
