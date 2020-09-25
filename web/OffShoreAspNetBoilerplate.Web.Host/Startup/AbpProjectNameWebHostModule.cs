using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OffShoreAspNetBoilerplate.Web.Core;
using OffShoreAspNetBoilerplate.Web.Core.Configuration;

namespace OffShoreAspNetBoilerplate.Web.Host.Startup
{
    [DependsOn(
       typeof(AbpProjectNameWebCoreModule))]
    public class AbpProjectNameWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AbpProjectNameWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameWebHostModule).GetAssembly());
        }
    }
}
