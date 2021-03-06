﻿namespace Linn.Production.Facade.Tests.WorksOrderServiceSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingWorksOrder : ContextBase
    {
        private WorksOrderResource resource;

        private IResult<WorksOrder> result;

        private WorksOrder worksOrder;

        private int raisedBy;

        [SetUp]
        public void SetUp()
        {
            this.raisedBy = 33067;

            this.resource = new WorksOrderResource
                                {
                                    PartNumber = "MAJIK",
                                    RaisedByDepartment = "DEPT",
                                    DocType = "DOC",
                                    Quantity = 3,
                                    KittedShort = "KIT",
                                    Links = new[] { new LinkResource("raised-by", $"/employees/{this.raisedBy}") }
                                };

            this.worksOrder = new WorksOrder
                                  {
                                      PartNumber = this.resource.PartNumber,
                                      RaisedByDepartment = this.resource.RaisedByDepartment,
                                      DocType = this.resource.DocType,
                                      RaisedBy = this.raisedBy,
                                      Quantity = this.resource.Quantity
                                  };

            this.WorksOrderFactory.RaiseWorksOrder(Arg.Any<WorksOrder>())
                .Returns(this.worksOrder);

            this.result = this.Sut.AddWorksOrder(this.resource);
        }

        [Test]
        public void ShouldRaiseWorksOrder()
        {
            this.WorksOrderFactory.Received().RaiseWorksOrder(Arg.Any<WorksOrder>());
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WorksOrderRepository.Received().Add(this.worksOrder);
        }

        [Test]
        public void ShouldIssueSernos()
        {
            this.WorksOrderUtilities.Received().IssueSerialNumber(
                this.resource.PartNumber,
                this.resource.OrderNumber,
                this.resource.DocType,
                this.resource.RaisedBy,
                this.resource.Quantity);
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<WorksOrder>>();
            var dataResult = ((CreatedResult<WorksOrder>)this.result).Data;
            dataResult.PartNumber.Should().Be("MAJIK");
        }
    }
}
