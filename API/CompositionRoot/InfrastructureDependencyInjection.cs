using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Infrastructure.Databse.Context;
using Infrastructure.Queries.Cars;
using Infrastructure.Queries.Users;
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
            await database.DisposeAsync();

            services.AddScoped<IDbContext>(_ =>
                new SiGManagerDB(connectionString));

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

            services.AddScoped<UserQuery>();

            services.AddScoped<IUserQuery>(sp =>
            {
                var inner = sp.GetRequiredService<UserQuery>();
                var logger = sp.GetRequiredService<ILogger<UserQuery>>();

                return new UserQueryDecorator(inner, logger);
            });

            services.AddScoped<CarRepository>();

            services.AddScoped<ICarRepository>(sp =>
            {
                var inner = sp.GetRequiredService<CarRepository>();
                var logger = sp.GetRequiredService<ILogger<CarRepository>>();

                return new CarRepositoryDecorator(inner, logger);
            });

            services.AddScoped<CarQuery>();

            services.AddScoped<ICarQuery>(sp =>
            {
                var inner = sp.GetRequiredService<CarQuery>();
                var logger = sp.GetRequiredService<ILogger<CarQuery>>();

                return new CarQueryDecorator(inner, logger);
            });

            return services;
        }
    }
}
