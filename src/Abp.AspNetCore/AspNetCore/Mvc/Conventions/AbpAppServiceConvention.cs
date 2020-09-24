using System;
using Abp.AspNetCore.Configuration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.AspNetCore.Mvc.Conventions
{
    public class AbpAppServiceConvention : IApplicationModelConvention
    {
        private readonly Lazy<AbpAspNetCoreConfiguration> _configuration;

        public AbpAppServiceConvention(IServiceCollection services)
        {
            _configuration = new Lazy<AbpAspNetCoreConfiguration>(() =>
            {
                return services
                    .GetSingletonService<AbpBootstrapper>()
                    .IocManager
                    .Resolve<AbpAspNetCoreConfiguration>();
            }, true);
        }

        public void Apply(ApplicationModel application)
        {

        }
    }
}
