using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Interfaces.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
    {
        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = serviceProvider.GetRequiredService(handlerType);

            return await handler.HandleAsync((dynamic)query);
        }
    }
}
