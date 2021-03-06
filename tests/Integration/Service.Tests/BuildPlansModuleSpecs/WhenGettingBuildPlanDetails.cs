﻿namespace Linn.Production.Service.Tests.BuildPlansModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.BuildPlans;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBuildPlanDetails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var b1 = new BuildPlanDetail { BuildPlanName = "name", PartNumber = "p1" };
            var b2 = new BuildPlanDetail { BuildPlanName = "name", PartNumber = "p2" };

            this.BuildPlanDetailsFacadeService.Search("name", Arg.Any<IEnumerable<string>>()).Returns(
                new SuccessResult<ResponseModel<IEnumerable<BuildPlanDetail>>>(
                    new ResponseModel<IEnumerable<BuildPlanDetail>>(
                        new List<BuildPlanDetail> { b1, b2 },
                        new List<string>())));

            this.Response = this.Browser.Get(
                "/production/maintenance/build-plan-details",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("buildPlanName", "name");
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
            this.BuildPlanDetailsFacadeService.Received().Search("name", Arg.Any<IEnumerable<string>>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<BuildPlanDetail>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(r => r.BuildPlanName == "name");
            resources.Should().Contain(r => r.PartNumber == "p1");
            resources.Should().Contain(r => r.PartNumber == "p2");
        }
    }
}
