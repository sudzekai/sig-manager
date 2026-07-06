using Application.Handlers.Bash.Objects;

namespace Application.Handlers.Bash.Interfaces
{
    internal interface IBashCommandHandler
    {
        string CommandName { get; }

        Task<string> HandleAsync(string[] args);
    }
}
