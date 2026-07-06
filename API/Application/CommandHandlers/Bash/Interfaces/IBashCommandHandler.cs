namespace Application.CommandHandlers.Bash.Interfaces
{
    internal interface IBashCommandHandler
    {
        string CommandName { get; }

        Task<string> HandleAsync(string[] args);
    }
}
