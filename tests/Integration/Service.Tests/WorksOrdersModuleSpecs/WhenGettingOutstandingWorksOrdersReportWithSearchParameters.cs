﻿namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingOutstandingWorksOrdersReportWithSearchParameters : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var results = new ResultsModel(new[] { "col1" });
            this.OutstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport("part-number", "MCP%201717/PP").Returns(
                new SuccessResult<ResultsModel>(results)
                    {
                        Data = new ResultsModel
                                   {
                                       ReportTitle =
                                           new NameModel("title")
                                   }
                    });

            this.Response = this.Browser.Get(
                "/production/works-orders/outstanding-works-orders-report",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("reportType", "part-number");
                        with.Query("searchParameter", "MCP%201717/PP");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.OutstandingWorksOrdersReportFacade.Received().GetOutstandingWorksOrdersReport("part-number", "MCP%201717/PP");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ReportReturnResource>();
            resource.ReportResults.First().title.displayString.Should().Be("title");
        }
    }
}