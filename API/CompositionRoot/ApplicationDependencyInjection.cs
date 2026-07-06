using Application;
using Application.Handlers.Bash;
using Application.Handlers.Cars.Get;
using Application.Handlers.Cars.Update;
using Application.Handlers.Cars.Write;
using Application.Handlers.Users.Get;
using Application.Handlers.Users.Update;
using Application.Handlers.Users.Write;
using Application.Services;
using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Interfaces.Application.Services;
using Contracts.Objects;
using Contracts.Objects.Commands.Bash;
using Contracts.Objects.Commands.Cars.Get;
using Contracts.Objects.Commands.Cars.Update;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Commands.Users.Get;
using Contracts.Objects.Commands.Users.Update;
using Contracts.Objects.Commands.Users.Write;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.User;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionRoot
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddSingleton<IBanIpService, BanIpService>();
            services.AddScoped<IHashService, HashService>();

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddCommandHandlers();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
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
            services.AddScoped<ICommandHandler<UserGetAllCommand, IReadOnlyList<UserSimpleDto>>, 
                               UserGetAllHandler>()
                    .AddScoped<ICommandHandler<UserGetCommand, UserInfoDto>, 
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
            services.AddScoped<ICommandHandler<CarGetAllCommand, IReadOnlyList<CarSimpleDto>>, 
                               CarGetAllHandler>()
                    .AddScoped<ICommandHandler<CarGetCommand, CarInfoDto>, 
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
