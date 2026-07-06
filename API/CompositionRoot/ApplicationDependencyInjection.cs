using Application;
using Application.CommandHandlers.Bash;
using Application.CommandHandlers.Cars.Update;
using Application.CommandHandlers.Cars.Write;
using Application.CommandHandlers.Users.Update;
using Application.CommandHandlers.Users.Write;
using Application.QueryHandlers.Cars;
using Application.QueryHandlers.Users;
using Application.Services;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Application.Services;
using Contracts.Objects;
using Contracts.Objects.Commands.Bash;
using Contracts.Objects.Commands.Cars.Update;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Commands.Users.Update;
using Contracts.Objects.Commands.Users.Write;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.User;
using Contracts.Objects.Queries.Cars;
using Contracts.Objects.Queries.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionRoot
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddSingleton<IBanIpService, BanIpService>();
            services.AddScoped<IHashService, HashService>();


            services.AddCommandHandlers();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            services.AddBashHandlers()
                    .AddUserHandlers()
                    .AddCarHandlers();

            return services;
        }

        private static IServiceCollection AddBashHandlers(this IServiceCollection services)
            => services.AddScoped<ICommandHandler<BashExecuteCommand, string>,
                                  BashCommandHandler>();

        private static IServiceCollection AddUserHandlers(this IServiceCollection services)
        =>  // get
            services.AddScoped<IQueryHandler<UserGetAllQuery, IReadOnlyList<UserSimpleDto>>, 
                               UserGetAllHandler>()
                    .AddScoped<IQueryHandler<UserGetQuery, UserInfoDto>, 
                               UserGetHandler>()
            // update
                    .AddScoped<ICommandHandler<UserInfoUpdateCommand, Unit>,
                               UserInfoUpdateHandler>()
                    .AddScoped<ICommandHandler<UserRoleUpdateCommand, Unit>,
                               UserRoleUpdateHandler>()
                    .AddScoped<ICommandHandler<UserPasswordUpdateCommand, Unit>,
                               UserPasswordUpdateHandler>()
            // write
                    .AddScoped<ICommandHandler<UserDeleteCommand, Unit>,
                               UserDeleteHandler>()
                    .AddScoped<ICommandHandler<UserCreateCommand, UserInfoDto>,
                               UserCreateHandler>();

        private static IServiceCollection AddCarHandlers(this IServiceCollection services)
        =>  // get
            services.AddScoped<IQueryHandler<CarGetAllQuery, IReadOnlyList<CarSimpleDto>>, 
                               CarGetAllHandler>()
                    .AddScoped<IQueryHandler<CarGetQuery, CarInfoDto>, 
                               CarGetHandler>()
            // update
                    .AddScoped<ICommandHandler<CarInfoUpdateCommand, Unit>,
                               CarInfoUpdateHandler>()
                    .AddScoped<ICommandHandler<CarStatusUpdateCommand, Unit>,
                               CarStatusUpdateHandler>()
            // write
                    .AddScoped<ICommandHandler<CarDeleteCommand, Unit>,
                               CarDeleteHandler>()
                    .AddScoped<ICommandHandler<CarCreateCommand, CarInfoDto>,
                               CarCreateHandler>();
    }
}
