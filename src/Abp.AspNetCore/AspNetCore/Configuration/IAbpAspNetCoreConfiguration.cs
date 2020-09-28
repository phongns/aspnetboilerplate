using System.Reflection;

namespace Abp.AspNetCore.Configuration
{
    public interface IAbpAspNetCoreConfiguration
    {
        AbpControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = AbpControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true
        );
    }
}
