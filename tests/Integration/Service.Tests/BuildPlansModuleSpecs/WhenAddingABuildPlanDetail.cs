﻿namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingABuildPlanDetail : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource =
                new BuildPlanDetailResource { BuildPlanName = "name", PartNumber = "part", FromLinnWeekNumber = 1 };

            this.AuthorisationService.HasPermissionFor(AuthorisedAction.BuildPlanDetailAdd, Arg.Any<List<string>>())
                .Returns(true);

            var buildPlanDetail =
                new BuildPlanDetail { BuildPlanName = "name", PartNumber = "part", FromLinnWeekNumber = 1 };

            this.BuildPlanDetailsFacadeService.Add(Arg.Any<BuildPlanDetailResource>(), Arg.Any<IEnumerable<string>>())
                .Returns(
                    new CreatedResult<ResponseModel<BuildPlanDetail>>(
                        new ResponseModel<BuildPlanDetail>(buildPlanDetail, new List<string>())));

            this.Response = this.Browser.Post(
                "/production/maintenance/build-plan-details",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.BuildPlanDetailsFacadeService.Received().Add(
                Arg.Any<BuildPlanDetailResource>(),
                Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<BuildPlanDetailResource>();
            resource.BuildPlanName.Should().Be("name");
            resource.PartNumber.Should().Be("part");
            resource.FromLinnWeekNumber.Should().Be(1);
        }
    }
}