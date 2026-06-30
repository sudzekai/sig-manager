using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Infrastructure.Databse.Context;
using Infrastructure.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionRoot
{
    public static class InfrastructureDependencyInjection
    {
        public static async Task<IServiceCollection> AddSingletonDatabaseAsync(this IServiceCollection services, string connectionString)
        {
            var database = new SiGManagerDB(connectionString);
            await database.TestConnectionAsync();

            services.AddSingleton<IDbContext>(database);

            return services;
        }

        public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
        {
            services.AddSingleton<UserRepository>();

            services.AddSingleton<IUserRepository>(sp =>
            {
                var inner = sp.GetRequiredService<UserRepository>();

                return new UserRepositoryDecorator(inner);
            });

            return services;
        }
    }
}
