using FP.BL.Services;
using FP.Common.ConfigurationModels;
using FP.Interfaces.Account;
using FP.Interfaces.Category;
using FP.Interfaces.Common.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FP.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();

            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind("Jwt", jwtConfiguration);
            services.AddSingleton<IJwtConfiguration>(jwtConfiguration);

            return services;
        }
    }
}
