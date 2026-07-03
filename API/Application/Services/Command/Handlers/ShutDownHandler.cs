using Microsoft.Extensions.Hosting;

namespace Application.Services.Command.Handlers
{
    internal class ShutDownHandler(IHostApplicationLifetime hostApplicationLifetime) : ICommandHandler
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;

        public string CommandName { get; } = "exit";

        public Task<string> HandleAsync(string[] args)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                _hostApplicationLifetime.StopApplication();
            });

            return Task.FromResult("завершение работы...");
        }
    }
}
