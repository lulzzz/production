﻿namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;
    using System.Collections.Generic;

    public class Cit
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string BuildGroup { get; set; }

        public int? SortOrder { get; set; }

        public string DepartmentCode { get; set; }


        public DateTime? DateInvalid { get; set; }

        public ProductionMeasures Measures { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }
    }
}
