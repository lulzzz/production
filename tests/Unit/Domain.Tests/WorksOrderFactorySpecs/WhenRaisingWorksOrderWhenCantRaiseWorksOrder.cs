﻿namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRaisingWorksOrderWhenCantRaiseWorksOrder : ContextBase
    {
        private Action action;

        private string department;

        private string partNumber;

        private int raisedBy;

        private string workStationCode;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "MAJIK";
            this.department = "DEPT";
            this.raisedBy = 33067;
            this.workStationCode = "STATION";

            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { BomType = "A", AccountingCompany = "LINN" });

            this.WorksOrderService.CanRaiseWorksOrder(this.partNumber).Returns("Error");

            this.action = () => this.Sut.RaiseWorksOrder(new WorksOrder
                                                             {
                                                                 PartNumber = this.partNumber,
                                                                 RaisedByDepartment = this.department,
                                                                 RaisedBy = this.raisedBy,
                                                                 WorkStationCode = this.workStationCode
                                                             });
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<DomainException>().WithMessage($"Error");
        }
    }
}