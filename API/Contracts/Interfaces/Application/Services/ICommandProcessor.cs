using Contracts.Objects.Dtos;

namespace Contracts.Interfaces.Application.Services
{
    public interface ICommandProcessor
    {
        Task<string> ProcessAsync(CommandDto command);
    }
}