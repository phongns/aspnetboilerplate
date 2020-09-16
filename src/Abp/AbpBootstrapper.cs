using System;

namespace Abp
{
    /// <summary>
    /// This is the main class that is responsible to start entire ABP system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class AbpBootstrapper : IDisposable
    {

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            //ResolveLogger();

            //try
            //{
            //    RegisterBootstrapper();
            //    IocManager.IocContainer.Install(new AbpCoreInstaller());

            //    IocManager.Resolve<AbpPlugInManager>().PlugInSources.AddRange(PlugInSources);
            //    IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            //    _moduleManager = IocManager.Resolve<AbpModuleManager>();
            //    _moduleManager.Initialize(StartupModule);
            //    _moduleManager.StartModules();
            //}
            //catch (Exception ex)
            //{
            //    _logger.Fatal(ex.ToString(), ex);
            //    throw;
            //}

            //AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            //if (IsDisposed)
            //{
            //    return;
            //}

            //IsDisposed = true;

            //_moduleManager?.ShutdownModules();
        }
    }
}
