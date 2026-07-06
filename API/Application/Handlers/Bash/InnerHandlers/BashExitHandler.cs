using Application.Handlers.Bash.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Application.Handlers.Bash.InnerHandlers
{
    internal class BashExitHandler(IHostApplicationLifetime hostApplicationLifetime) : IBashCommandHandler
    {
        public string CommandName { get; } = "exit";

        public Task<string> HandleAsync(string[] args)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                hostApplicationLifetime.StopApplication();
            });

            return Task.FromResult("завершение работы...");
        }
    }
}
