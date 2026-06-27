namespace Application.Services.Command.Handlers
{
    internal interface ICommandHandler
    {
        string CommandName { get; }
        Task<string> HandleAsync(string[] args);
    }
}
