﻿namespace Linn.Production.Service.Tests.ManufacturingResourcesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.ResourceBuilders;
    using Linn.Production.Resources;
    using Linn.Production.Service.Modules;
    using Linn.Production.Service.ResponseProcessors;
    using Linn.Production.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource> ManufacturingResourceService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ManufacturingResourceService = Substitute.For<IFacadeService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ManufacturingResourceService);
                    with.Dependency<IResourceBuilder<ManufacturingResource>>(new ManufacturingResourceResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ManufacturingResource>>>(
                        new ManufacturingResourcesResourceBuilder());
                    with.Module<ManufacturingResourceModule>();
                    with.ResponseProcessor<ManufacturingResourcesResponseProcessor>();
                    with.ResponseProcessor<ManufacturingResourceResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                 };

                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
