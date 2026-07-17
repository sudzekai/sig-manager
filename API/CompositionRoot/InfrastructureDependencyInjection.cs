using Contracts.Interfaces.Infrastructure;
using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.Databse.Context;
using Infrastructure.Queries.Cars;
using Infrastructure.Queries.CarShifts;
using Infrastructure.Queries.Users;
using Infrastructure.Repositories.Cars;
using Infrastructure.Repositories.CarShifts;
using Infrastructure.Repositories.InfoShifts;
using Infrastructure.Repositories.Positions;
using Infrastructure.Repositories.Shifts;
using Infrastructure.Repositories.TicketShifts;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.UserShifts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompositionRoot
{
    public static class InfrastructureDependencyInjection
    {
        public static async Task<IServiceCollection> AddDatabase(this IServiceCollection services, string connectionString)
        {
            var database = new SiGManagerDB(connectionString, null);
            await database.TestConnectionAsync();
            await database.DisposeAsync();

            services.AddScoped<IDbContext>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SiGManagerDB>>();
                return new SiGManagerDB(connectionString, logger);
            });

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
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

            services.AddScoped<PositionRepository>();

            services.AddScoped<IPositionRepository>(sp =>
            {
                var inner = sp.GetRequiredService<PositionRepository>();
                var logger = sp.GetRequiredService<ILogger<PositionRepository>>();

                return new PositionRepositoryDecorator(inner, logger);
            });

            services.AddScoped<CarRepository>();

            services.AddScoped<ICarRepository>(sp =>
            {
                var inner = sp.GetRequiredService<CarRepository>();
                var logger = sp.GetRequiredService<ILogger<CarRepository>>();

                return new CarRepositoryDecorator(inner, logger);
            });

            services.AddScoped<UserShiftRepository>();

            services.AddScoped<IUserShiftRepository>(sp =>
            {
                var inner = sp.GetRequiredService<UserShiftRepository>();
                var logger = sp.GetRequiredService<ILogger<UserShiftRepository>>();

                return new UserShiftRepositoryDecorator(inner, logger);
            });

            services.AddScoped<ShiftRepository>();

            services.AddScoped<IShiftRepository>(sp =>
            {
                var inner = sp.GetRequiredService<ShiftRepository>();
                var logger = sp.GetRequiredService<ILogger<ShiftRepository>>();

                return new ShiftRepositoryDecorator(inner, logger);
            });

            services.AddScoped<CarShiftRepository>();

            services.AddScoped<ICarShiftRepository>(sp =>
            {
                var inner = sp.GetRequiredService<CarShiftRepository>();
                var logger = sp.GetRequiredService<ILogger<CarShiftRepository>>();

                return new CarShiftRepositoryDecorator(inner, logger);
            });

            services.AddScoped<InfoShiftRepository>();

            services.AddScoped<IInfoShiftRepository>(sp =>
            {
                var inner = sp.GetRequiredService<InfoShiftRepository>();
                var logger = sp.GetRequiredService<ILogger<InfoShiftRepository>>();

                return new InfoShiftRepositoryDecorator(inner, logger);
            });

            services.AddScoped<TicketShiftsRepository>();

            services.AddScoped<ITicketShiftRepository>(sp =>
            {
                var inner = sp.GetRequiredService<TicketShiftsRepository>();
                var logger = sp.GetRequiredService<ILogger<TicketShiftsRepository>>();

                return new TicketShiftRepositoryDecorator(inner, logger);
            });

 

            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<UserQuery>();

            services.AddScoped<IUserQuery>(sp =>
            {
                var inner = sp.GetRequiredService<UserQuery>();
                var logger = sp.GetRequiredService<ILogger<UserQuery>>();

                return new UserQueryDecorator(inner, logger);
            });

            services.AddScoped<CarQuery>();

            services.AddScoped<ICarQuery>(sp =>
            {
                var inner = sp.GetRequiredService<CarQuery>();
                var logger = sp.GetRequiredService<ILogger<CarQuery>>();

                return new CarQueryDecorator(inner, logger);
            });

            services.AddScoped<CarShiftQuery>();

            services.AddScoped<ICarShiftQuery>(sp =>
            {
                var inner = sp.GetRequiredService<CarShiftQuery>();
                var logger = sp.GetRequiredService<ILogger<CarShiftQuery>>();

                return new CarShiftQueryDecorator(inner, logger);
            });

            return services;
        }
    }
}
