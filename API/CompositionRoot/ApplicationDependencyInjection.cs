using Application.Services;
using Application.Services.Cars;
using Application.Services.Command;
using Application.Services.Users;
using Contracts.Interfaces.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompositionRoot
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IBanIpService, BanIpService>();

            services.AddScoped<ICommandProcessor, CommandProcessor>();
            services.AddScoped<IHashService, HashService>();

            services.AddScoped<UsersService>();

            services.AddScoped<IUsersService>(sp =>
            {
                var inner = sp.GetRequiredService<UsersService>();
                var logger = sp.GetRequiredService<ILogger<UsersService>>();

                return new UsersServiceDecorator(inner);
            });

            services.AddScoped<CarsService>();

            services.AddScoped<ICarsService>(sp =>
            {
                var inner = sp.GetRequiredService<CarsService>();
                var logger = sp.GetRequiredService<ILogger<CarsService>>();

                return new CarsServiceDecorator(inner);
            });

            return services;
        }
    }
}
