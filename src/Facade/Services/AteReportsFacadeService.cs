﻿namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Exceptions;

    public class AteReportsFacadeService : IAteReportsFacadeService
    {
        private readonly IAteReportsService ateReportsService;

        public AteReportsFacadeService(IAteReportsService ateReportsService)
        {
            this.ateReportsService = ateReportsService;
        }

        public IResult<ResultsModel> GetStatusReport(
            string resourceFromDate,
            string resourceToDate,
            string resourceSmtOrPcb,
            string resourcePlaceFound)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = this.ConvertDate(resourceFromDate);
                to = this.ConvertDate(resourceToDate);
            }
            catch (InvalidDateException exception)
            {
                return new BadRequestResult<ResultsModel>(exception.Message);
            }

            return new SuccessResult<ResultsModel>(
                this.ateReportsService.GetStatusReport(from, to, resourceSmtOrPcb, resourcePlaceFound));
        }

        public IResult<ResultsModel> GetDetailsReport(string fromDate, string toDate, string selectBy, string value)
        {
            throw new System.NotImplementedException();
        }

        private DateTime ConvertDate(string isoDate)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(isoDate);
            }
            catch (Exception)
            {
                throw new InvalidDateException("Invalid dates supplied to ate report");
            }

            return date;
        }
    }
}