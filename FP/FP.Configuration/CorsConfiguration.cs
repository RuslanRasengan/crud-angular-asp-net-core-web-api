using Microsoft.Extensions.DependencyInjection;

namespace FP.Configuration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(
                "CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

            return services;
        }
    }
}
