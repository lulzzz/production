﻿namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected WorksOrderUtilities Sut { get; private set; }

        protected IWorksOrderProxyService WorksOrderService { get; private set; }

        protected IRepository<Part, string> PartsRepository { get; private set; }

        protected IRepository<ProductionTriggerLevel, string> ProductionTriggerLevelsRepository { get; private set; }

        protected IRepository<PcasBoardForAudit, string> PcasBoardsForAuditRepository { get; private set; }

        protected IRepository<PcasRevision, string> PcasRevisionsRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderService = Substitute.For<IWorksOrderProxyService>();
            this.PartsRepository = Substitute.For<IRepository<Part, string>>();
            this.ProductionTriggerLevelsRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.PcasBoardsForAuditRepository = Substitute.For<IRepository<PcasBoardForAudit, string>>();
            this.PcasRevisionsRepository = Substitute.For<IRepository<PcasRevision, string>>();
            this.SernosPack = Substitute.For<ISernosPack>();

            this.Sut = new WorksOrderUtilities(
                this.PartsRepository,
                this.PcasBoardsForAuditRepository,
                this.PcasRevisionsRepository,
                this.ProductionTriggerLevelsRepository,
                this.WorksOrderService,
                this.SernosPack);
        }
    }
}