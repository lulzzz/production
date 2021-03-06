﻿namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ATE;

    public class AteFaultCodeResponseProcessor : JsonResponseProcessor<AteFaultCode>
    {
        public AteFaultCodeResponseProcessor(IResourceBuilder<AteFaultCode> resourceBuilder)
            : base(resourceBuilder, "ate-fault-code", 1)
        {
        }
    }
}