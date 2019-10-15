﻿namespace Linn.Production.Service.Modules.Reports
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class OrdersReportsModule : NancyModule
    {
        private readonly IOrdersReportsFacadeService reportService;

        public OrdersReportsModule(IOrdersReportsFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/production/reports/manufacturing-commit-date/report", _ => this.ManufacturingCommitDateReport());
            this.Get("/production/reports/manufacturing-commit-date", _ => this.ManufacturingCommitDateReportOptions());
        }

        private object ManufacturingCommitDateReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object ManufacturingCommitDateReport()
        {
            var resource = this.Bind<DateRequestResource>();
            var results = this.reportService.ManufacturingCommitDateReport(resource.Date);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}                                                                              