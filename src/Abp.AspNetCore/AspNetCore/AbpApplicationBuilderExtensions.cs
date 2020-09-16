using System;
using Abp.AspNetCore.Security;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Abp.AspNetCore
{
    public static class AbpApplicationBuilderExtensions
    {
        private const string AuthorizationExceptionHandlingMiddlewareMarker = "_AbpAuthorizationExceptionHandlingMiddleware_Added";

        public static void UseAbp(this IApplicationBuilder app)
        {
            app.UseAbp(null);
        }

        public static void UseAbp([NotNull] this IApplicationBuilder app, Action<AbpApplicationBuilderOptions> optionsAction)
        {
            //Check.NotNull(app, nameof(app));
            var options = new AbpApplicationBuilderOptions();
            optionsAction?.Invoke(options);

            if (options.UseCastleLoggerFactory)
            {
                //app.UseCastleLoggerFactory();
            }

            //InitializeAbp(app);

            if (options.UseSecurityHeaders)
            {
                app.UseAbpSecurityHeaders();
            }
        }

        //private void AddInterceptorRegistrars()
        //{
        //    //ValidationInterceptorRegistrar.Initialize(IocManager);
        //    //AuditingInterceptorRegistrar.Initialize(IocManager);
        //    //EntityHistoryInterceptorRegistrar.Initialize(IocManager);
        //    //UnitOfWorkRegistrar.Initialize(IocManager);
        //    //AuthorizationInterceptorRegistrar.Initialize(/*IocManager*/);
        //}

        private static void InitializeAbp(IApplicationBuilder app)
        {
            var abpBootstrapper = app.ApplicationServices.GetRequiredService<AbpBootstrapper>();
            abpBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => abpBootstrapper.Dispose());
        }

        public static void UseAbpSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<AbpSecurityHeadersMiddleware>();
        }
    }
}
