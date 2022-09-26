using AspNetCoreRateLimit;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.Extensions;
using CompanyEmployees.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepoManager();

            // Api versioning
            services.ConfigureVersioning();

            // rate limit and throttling
            // ASP NET CORE RATE LIMIT library uses memory cache

            services.AddMemoryCache();

            services.ConfigureRateLimit();
            services.AddHttpContextAccessor();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            // registration of action filters
            services.AddScoped<ValidationActionAttribute>();

            // for returning a different status code when validation fails
            // remove [ApiController] from the controller class
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            // registering the cache services
            services.ConfigureResponseCache();

            // model validation for cache
            services.ConfigureHttpCacheHeader();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(cf =>
            {
                cf.RespectBrowserAcceptHeader = true;
                cf.ReturnHttpNotAcceptable = true;

                // for cache profile
                cf.CacheProfiles.Add("cacheDuration", new CacheProfile { Duration = 120 });
            })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
                //.AddCustonCSVFormatter();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseMiddleware<CustomErrorExecption>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            // rate limiting
            app.UseIpRateLimiting();

            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
