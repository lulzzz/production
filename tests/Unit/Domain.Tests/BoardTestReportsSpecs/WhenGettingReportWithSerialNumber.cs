﻿namespace Linn.Production.Domain.Tests.BoardTestReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BoardTests;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReportWithSerialNumber : ContextBase
    {
        private ResultsModel results;

        [SetUp]
        public void SetUp()
        {
            var boardTests = new List<BoardTest>
                                 {
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A1",
                                             Seq = 1,
                                             TestMachine = "G1",
                                             Status = "FAIL",
                                             DateTested = 1.May(2021),
                                             FailType = new BoardFailType { Type = 1 }
                                         },
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A2",
                                             Seq = 2,
                                             TestMachine = "G1",
                                             Status = "FAIL",
                                             DateTested = 1.May(2021),
                                             FailType = new BoardFailType { Type = 2 }
                                         },
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A2",
                                             Seq = 3,
                                             TestMachine = "G1",
                                             Status = "PASS",
                                             DateTested = 2.May(2021)
                                         }
                                 };
            this.BoardTestRepository.FilterBy(Arg.Any<Expression<Func<BoardTest, bool>>>())
                .Returns(boardTests.AsQueryable());
            this.results = this.Sut.GetBoardTestReport(1.May(2020), 31.May(2020), "1");
        }

        [Test]
        public void ShouldGetData()
        {
            this.BoardTestRepository.Received().FilterBy(Arg.Any<Expression<Func<BoardTest, bool>>>());
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Rows.Should().HaveCount(1);
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("Board Name")).Should().Be("A2");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("Board Serial Number")).Should().Be("1");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("First Test Date")).Should().Be("01-May-2021");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("Last Test Date")).Should().Be("02-May-2021");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("No Of Tests")).Should().Be("3");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("Passed At Test")).Should().Be("3");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("Status")).Should().Be("PASS");
        }
    }
}