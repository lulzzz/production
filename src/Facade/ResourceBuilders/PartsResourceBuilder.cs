﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartsResourceBuilder : IResourceBuilder<IEnumerable<Part>>
    {
        private readonly PartResourceBuilder partResourceBuilder = new PartResourceBuilder();

        public IEnumerable<PartResource> Build(IEnumerable<Part> parts)
        {
            return parts
                .OrderBy(b => b.PartNumber)
                .Select(a => this.partResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Part>>.Build(IEnumerable<Part> parts) => this.Build(parts);

        public string GetLocation(IEnumerable<Part> parts)
        {
            throw new NotImplementedException();
        }
    }
}