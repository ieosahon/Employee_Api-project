using AspNetCoreRateLimit;
using Contracts;
using Entities;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Collections.Generic;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(opt =>
           {
               opt.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   );
           });

        public static void ConfigureIISIntegration(this IServiceCollection servicess) =>
            servicess.Configure<IISOptions>(opt =>
            {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<RepoContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("EmployeeApiConn"), db => db.MigrationsAssembly("CompanyEmployees")
        ));

        public static void ConfigureRepoManager(this IServiceCollection services) =>
            services.AddScoped<IRepoManager, RepoManager>();

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        /// <summary>
        /// implementing cache store
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureResponseCache(this IServiceCollection services) => services.AddResponseCaching();

        public static void ConfigureHttpCacheHeader(this IServiceCollection services) =>
            services.AddHttpCacheHeaders((eOpt) =>
            {
                eOpt.MaxAge = 70;
                eOpt.CacheLocation = CacheLocation.Public;
            },

            (vOpt) =>
             {
                 vOpt.MustRevalidate = true;
             });

        /// <summary>
        /// to implement rate limit
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRateLimit(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 5,
                    Period = "1m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
