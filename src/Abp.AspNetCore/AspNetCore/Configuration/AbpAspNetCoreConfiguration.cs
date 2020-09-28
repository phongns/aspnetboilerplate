using System.Reflection;

namespace Abp.AspNetCore.Configuration
{
    public class AbpAspNetCoreConfiguration : IAbpAspNetCoreConfiguration
    {
        public ControllerAssemblySettingList ControllerAssemblySettings { get; }

        public AbpAspNetCoreConfiguration()
        {

            ControllerAssemblySettings = new ControllerAssemblySettingList();
        }

        public AbpControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = AbpControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true)
        {
            var setting = new AbpControllerAssemblySetting(moduleName, assembly, useConventionalHttpVerbs);
            ControllerAssemblySettings.Add(setting);
            return new AbpControllerAssemblySettingBuilder(setting);
        }
    }
}
