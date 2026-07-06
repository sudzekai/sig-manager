using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;

namespace Contracts.Interfaces.Application.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
