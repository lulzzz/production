﻿namespace Linn.Production.Resources
{
    public class ProductionMeasuresResource
    {
        public string CitCode { get; set; }

        public string CitName { get; set; }

        public string CitBuildGroup { get; set; }

        public int? Ones { get; set; }

        public int? Twos { get; set; }

        public int? Threes { get; set; }

        public int? Fours { get; set; }

        public int? Fives { get; set; }

        public double? BuiltThisWeekValue { get; set; }

        public int? BuiltThisWeekQty { get; set; }

        public double? BackOrderValue { get; set; }

        public double? FFlaggedValue { get; set; }

        public double? FFlaggedQty { get; set; }

        public double? StockValue { get; set; }

        public double? OverStockValue { get; set; }

        public int? NumberOfBackOrders { get; set; }

        public int? NumberOfPartsBackOrdered { get; set; }

        public string OldestBackOrder { get; set; }

        public string PtlJobref { get; set; }

        public string PboJobref { get; set; }

        public double? DaysRequired { get; set; }

        public double? DaysRequired3 { get; set; }

        public double? DaysRequiredCanDo12 { get; set; }

        public double? DaysRequiredCanDo3 { get; set; }

        public int? PboJobId { get; set; }

        public double? UsageValue { get; set; }

        public double? UsageForTotalValue { get; set; }

        public double? AvgStockValue { get; set; }

        public int? ShortBat { get; set; }

        public int? ShortMetalwork { get; set; }

        public int? ShortProc { get; set; }

        public int? ShortAny { get; set; }

        public double? DeliveryPerformance1s { get; set; }

        public double? DeliveryPerformance2s { get; set; }
    }
}