using System;
using System.Linq;
using System.Reflection;
using Abp.AspNetCore;
using Abp.Extensions;
using Abp.ZeroCore.Authorization.Roles;
using Abp.ZeroCore.Authorization.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OffShoreAspNetBoilerplate.Web.Core.Configuration;

namespace OffShoreAspNetBoilerplate.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private const string _apiVersion = "v1";

        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddControllersWithViews(
                options =>
                {
                    //options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                }
            );
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
            //    {
            //        NamingStrategy = new CamelCaseNamingStrategy()
            //    };
            //});

            services.AddIdentity<AbpUser, AbpRole>()
               .AddDefaultTokenProviders();
            AuthConfigurer.Configure(services, _appConfiguration);

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder.WithOrigins(
                    // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                    _appConfiguration["App:CorsOrigins"]
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray()
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                )
            );

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "OffShoreAspNetBoilerplate API",
                    Description = "OffShoreAspNetBoilerplate",
                    // uncomment if needed TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "OffShoreAspNetBoilerplate",
                        Email = string.Empty,
                        // Url = new Uri("https://twitter.com/aspboilerplate"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        //Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                    }
                });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            // Configure Abp and Dependency Injection
            return services.AddAbp<AbpProjectNameWebHostModule>(
            //// Configure Log4Net logging
            //options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    f => f.UseAbpLog4Net().WithConfig("log4net.config")
            //)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseRouting();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                // specifying the Swagger JSON endpoint.
                options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"OffShoreAspNetBoilerplate API {_apiVersion}");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("OffShoreAspNetBoilerplate.Web.Host.wwwroot.swagger.ui.index.html");
                options.DisplayRequestDuration(); // Controls the display of the request duration (in milliseconds) for "Try it out" requests.  
            }); // URL: /swagger
        }
    }
}
