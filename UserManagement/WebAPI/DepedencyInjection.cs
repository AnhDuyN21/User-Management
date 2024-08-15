using Application.Interfaces.IServices;
using Application.Interfaces;
using Infrastructures;
using System.Diagnostics;
using WebAPI.Services;
using Newtonsoft.Json;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.Formatting = Formatting.Indented;
               });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsService, ClaimsService>();


            services.AddSingleton<Stopwatch>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
