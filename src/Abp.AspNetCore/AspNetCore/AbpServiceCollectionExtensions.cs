using System;
using Abp.Dependency;
using Abp.AspNetCore.Mvc;
using Castle.Windsor.MsDependencyInjection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Abp.Modules;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Abp.AspNetCore
{
    public static class AbpServiceCollectionExtensions
    {
        /// <summary>
        /// Integrates ABP to AspNet Core.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">An action to get/modify options</param>
        public static IServiceProvider AddAbp<TStartupModule>(this IServiceCollection services, [CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
            where TStartupModule : AbpModule
        {
            var abpBootstrapper = AddAbpBootstrapper<TStartupModule>(services, optionsAction);

            ConfigureAspNetCore(services, abpBootstrapper.IocManager);

            return WindsorRegistrationHelper.CreateServiceProvider(abpBootstrapper.IocManager.IocContainer, services);
        }

        private static void ConfigureAspNetCore(IServiceCollection services, IIocResolver iocResolver)
        {
            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create page models
            services.Replace(ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());

            //Use DI to create view components
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

            //Add feature providers
            var partManager = services.GetSingletonServiceOrNull<ApplicationPartManager>();
            //partManager?.FeatureProviders.Add(new AbpAppServiceControllerFeatureProvider(iocResolver));

            ////Configure JSON serializer
            //services.Configure<MvcNewtonsoftJsonOptions>(jsonOptions =>
            //{
            //    jsonOptions.SerializerSettings.ContractResolver = new AbpMvcContractResolver(iocResolver)
            //    {
            //        NamingStrategy = new CamelCaseNamingStrategy()
            //    };
            //});

            //Configure MVC
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddAbp(services);
            });
        }

        private static AbpBootstrapper AddAbpBootstrapper<TStartupModule>(IServiceCollection services, Action<AbpBootstrapperOptions> optionsAction)
            where TStartupModule : AbpModule
        {
            var abpBootstrapper = AbpBootstrapper.Create<TStartupModule>(optionsAction);
            services.AddSingleton(abpBootstrapper);
            return abpBootstrapper;
        }
    }
}
