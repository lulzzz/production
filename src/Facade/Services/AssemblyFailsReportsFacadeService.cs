﻿namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;

    public class AssemblyFailsReportsFacadeService : IAssemblyFailsReportsFacadeService
    {
        private readonly IAssemblyFailsReportService reportService;

        public AssemblyFailsReportsFacadeService(IAssemblyFailsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<ResultsModel> GetAssemblyFailsWaitingListReport()
        {
            return new SuccessResult<ResultsModel>(this.reportService.GetAssemblyFailsWaitingListReport());
        }

        public IResult<ResultsModel> GetAssemblyFailsMeasuresReport(string fromDate, string toDate)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(fromDate);
                to = DateTime.Parse(toDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to assembly fails measures report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetAssemblyFailsMeasuresReport(from, to, AssemblyFailGroupBy.partNumber));
        }
    }
}