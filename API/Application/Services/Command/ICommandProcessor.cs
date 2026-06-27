using Application.Objects.Dtos;

namespace Application.Services.Command
{
    public interface ICommandProcessor
    {
        Task<string> ProcessAsync(CommandDto command);
    }
}