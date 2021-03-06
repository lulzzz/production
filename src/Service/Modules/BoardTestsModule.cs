﻿namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class BoardTestsModule : NancyModule
    {
        private readonly IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> facadeService;

        private readonly IBoardTestReportFacadeService boardTestReportFacadeService;

        public BoardTestsModule(
            IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> facadeService,
            IBoardTestReportFacadeService boardTestReportFacadeService)
        {
            this.boardTestReportFacadeService = boardTestReportFacadeService;
            this.facadeService = facadeService;

            this.Get("/production/reports/board-tests-report", _ => this.GetBoardTestsReport());
            this.Get("/production/reports/board-test-details-report", _ => this.GetBoardTestDetailsReport());
            this.Get("/production/reports/board-tests-report/report", _ => this.GetApp());
            this.Get("/production/resources/board-fail-types", _ => this.GetAll());
            this.Get("/production/resources/board-fail-types/{type*}", parameters => this.GetById(parameters.type));
            this.Put("/production/resources/board-fail-types/{type*}", parameters => this.Update(parameters.type));
            this.Post("/production/resources/board-fail-types", parameters => this.Add());
        }

        private object GetBoardTestDetailsReport()
        {
            var resource = this.Bind<BoardTestRequestResource>();
            var result = this.boardTestReportFacadeService.GetBoardTestDetailsReport(resource.BoardId);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetBoardTestsReport()
        {
            var resource = this.Bind<BoardTestRequestResource>();
            var result = this.boardTestReportFacadeService.GetBoardTestReport(resource.FromDate, resource.ToDate, resource.BoardId);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetAll()
        {
            var result = this.facadeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(int id)
        {
            var result = this.facadeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Update(int type)
        {
            var resource = this.Bind<BoardFailTypeResource>();

            var result = this.facadeService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Add()
        {
            var resource = this.Bind<BoardFailTypeResource>();

            var result = this.facadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
