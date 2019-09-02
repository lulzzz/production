﻿namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;

    public interface ILinnWeekService
    {
        IEnumerable<LinnWeek> GetWeeks(DateTime startDate, DateTime endDate);

        LinnWeek GetWeek(DateTime date);
    }
}