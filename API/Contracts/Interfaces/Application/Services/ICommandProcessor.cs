using Contracts.Objects.Dtos.Models.Requests;

namespace Contracts.Interfaces.Application.Services
{
    public interface ICommandProcessor
    {
        Task<string> ProcessAsync(CommandDto command);
    }
}