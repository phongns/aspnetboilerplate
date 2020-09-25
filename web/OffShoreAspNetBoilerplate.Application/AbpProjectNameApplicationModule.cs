using System;
using System.Collections.Generic;
using System.Text;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace OffShoreAspNetBoilerplate
{
    [DependsOn(
        typeof(AbpProjectNameCoreModule)
        //typeof(AbpAutoMapperModule)
        )]
    public class AbpProjectNameApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Authorization.Providers.Add<AbpProjectNameAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AbpProjectNameApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            //Configuration.Modules.AbpAutoMapper().Configurators.Add(
            //    // Scan the assembly for classes which inherit from AutoMapper.Profile
            //    cfg => cfg.AddMaps(thisAssembly)
            //);
        }
    }
}
