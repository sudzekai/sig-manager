using Application.Services.Users;
using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Infrastructure.Databse.Context;
using Infrastructure.Repositories.Cars;
using Infrastructure.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompositionRoot
{
    public static class InfrastructureDependencyInjection
    {
        public static async Task<IServiceCollection> AddDatabase(this IServiceCollection services, string connectionString)
        {
            var database = new SiGManagerDB(connectionString);
            await database.TestConnectionAsync();

            services.AddSingleton<IDbContext>(database);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();

            services.AddScoped<IUserRepository>(sp =>
            {
                var inner = sp.GetRequiredService<UserRepository>();
                var logger = sp.GetRequiredService<ILogger<UserRepository>>();

                return new UserRepositoryDecorator(inner, logger);
            });

            services.AddScoped<CarRepository>();

            services.AddScoped<ICarRepository>(sp =>
            {
                var inner = sp.GetRequiredService<CarRepository>();
                var logger = sp.GetRequiredService<ILogger<CarRepository>>();

                return new CarRepositoryDecorator(inner, logger);
            });

            return services;
        }
    }
}
