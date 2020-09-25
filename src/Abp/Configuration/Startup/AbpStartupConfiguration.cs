using Abp.Dependency;

namespace Abp.Configuration.Startup
{
    /// <summary>
    /// This class is used to configure ABP and modules on startup.
    /// </summary>
    internal class AbpStartupConfiguration : DictionaryBasedConfig, IAbpStartupConfiguration
    {
        /// <summary>
        /// Reference to the IocManager.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Used to configure authorization.
        /// </summary>
        public IAuthorizationConfiguration Authorization { get; private set; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="ModuleConfigurations"/> to add module specific configurations.
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }

        /// <summary>
        /// Private constructor for singleton pattern.
        /// </summary>
        public AbpStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            //Localization = IocManager.Resolve<ILocalizationConfiguration>();
            Modules = IocManager.Resolve<IModuleConfigurations>();
            //Features = IocManager.Resolve<IFeatureConfiguration>();
            //Navigation = IocManager.Resolve<INavigationConfiguration>();
            Authorization = IocManager.Resolve<IAuthorizationConfiguration>();
            //Validation = IocManager.Resolve<IValidationConfiguration>();
            //Settings = IocManager.Resolve<ISettingsConfiguration>();
            //UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            //EventBus = IocManager.Resolve<IEventBusConfiguration>();
            //MultiTenancy = IocManager.Resolve<IMultiTenancyConfig>();
            //Auditing = IocManager.Resolve<IAuditingConfiguration>();
            //Caching = IocManager.Resolve<ICachingConfiguration>();
            //BackgroundJobs = IocManager.Resolve<IBackgroundJobConfiguration>();
            //Notifications = IocManager.Resolve<INotificationConfiguration>();
            //EmbeddedResources = IocManager.Resolve<IEmbeddedResourcesConfiguration>();
            //EntityHistory = IocManager.Resolve<IEntityHistoryConfiguration>();
            //Webhooks = IocManager.Resolve<IWebhooksConfiguration>();
            //DynamicEntityParameters = IocManager.Resolve<IDynamicEntityParameterConfiguration>();

            //CustomConfigProviders = new List<ICustomConfigProvider>();
            //ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
    }
}
