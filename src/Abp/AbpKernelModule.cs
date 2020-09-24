using Abp.Modules;

namespace Abp
{
    /// <summary>
    /// Kernel (core) module of the ABP system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class AbpKernelModule : AbpModule
    {
        public override void PreInitialize()
        {

        }

        public override void Initialize()
        {

        }

        private void RegisterInterceptors()
        {

        }

        public override void PostInitialize()
        {

        }

        public override void Shutdown()
        {

        }

        private void AddUnitOfWorkFilters()
        {

        }

        private void AddSettingProviders()
        {

        }

        private void AddAuditingSelectors()
        {

        }

        private void AddLocalizationSources()
        {

        }

        private void ConfigureCaches()
        {

        }

        private void AddIgnoredTypes()
        {

        }

        private void AddMethodParameterValidators()
        {

        }

        private void AddDefaultNotificationDistributor()
        {

        }

        private void RegisterMissingComponents()
        {

        }
    }
}
