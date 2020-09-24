using Abp.Configuration.Startup;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Abp.Dependency.Installers
{
    internal class AbpCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<IAbpStartupConfiguration, AbpStartupConfiguration>().ImplementedBy<AbpStartupConfiguration>().LifestyleSingleton(),
                Component.For<IAbpModuleManager, AbpModuleManager>().ImplementedBy<AbpModuleManager>().LifestyleSingleton()
                );
        }
    }
}
