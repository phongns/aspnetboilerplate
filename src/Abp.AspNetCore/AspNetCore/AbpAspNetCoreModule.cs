using System.Linq;
using Abp.AspNetCore.Configuration;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Web;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;

namespace Abp.AspNetCore
{
    [DependsOn(typeof(AbpWebCommonModule))]
    public class AbpAspNetCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpAspNetCoreConfiguration, AbpAspNetCoreConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpAspNetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            AddApplicationParts();
            ConfigureAntiforgery();
        }

        private void AddApplicationParts()
        {
            var configuration = IocManager.Resolve<AbpAspNetCoreConfiguration>();
            var partManager = IocManager.Resolve<ApplicationPartManager>();
            var moduleManager = IocManager.Resolve<IAbpModuleManager>();

            partManager.AddApplicationPartsIfNotAddedBefore(typeof(AbpAspNetCoreModule).Assembly);

            var controllerAssemblies = configuration.ControllerAssemblySettings.Select(s => s.Assembly).Distinct();
            foreach (var controllerAssembly in controllerAssemblies)
            {
                partManager.AddApplicationPartsIfNotAddedBefore(controllerAssembly);
            }

            var plugInAssemblies = moduleManager.Modules.Where(m => m.IsLoadedAsPlugIn).Select(m => m.Assembly).Distinct();
            foreach (var plugInAssembly in plugInAssemblies)
            {
                partManager.AddApplicationPartsIfNotAddedBefore(plugInAssembly);
            }
        }

        private void ConfigureAntiforgery()
        {
            IocManager.Using<IOptions<AntiforgeryOptions>>(optionsAccessor =>
            {
                //optionsAccessor.Value.HeaderName = Configuration.Modules.AbpWebCommon().AntiForgery.TokenHeaderName;
            });
        }
    }
}
