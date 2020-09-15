using System;
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
                //Guid.NewGuid()
            }

        }

        private static void InitializeAbp(IApplicationBuilder app)
        {
            var abpBootstrapper = app.ApplicationServices.GetRequiredService<AbpBootstrapper>();
            abpBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => abpBootstrapper.Dispose());
        }
    }
}
