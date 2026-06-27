using Application.Services.ApplicationLifetime;
using Application.Services.BanIp;
using Application.Services.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandProcessor, CommandProcessor>();
            services.AddSingleton<IBanIpService, BanIpService>();
            services.AddSingleton<ILifetimeService, LifetimeService>();

            return services;
        }
    }
}
