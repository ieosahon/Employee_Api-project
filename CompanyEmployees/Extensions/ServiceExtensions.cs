using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
