﻿namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWorksOrderByOrderNumber : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var worksOrder = new WorksOrder { OrderNumber = 1234, Part = new Part { Description = "DESC" } };

            this.WorksOrdersService.GetById(1234).Returns(new SuccessResult<WorksOrder>(worksOrder));

            this.Response = this.Browser.Get(
                "/production/works-orders/1234",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrdersService.Received().GetById(1234);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderResource>();
            resource.OrderNumber.Should().Be(1234);
        }
    }
}
