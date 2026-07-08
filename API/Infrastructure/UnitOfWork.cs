using Contracts.Interfaces.Infrastructure;
using Contracts.Interfaces.Infrastructure.Context;

namespace Infrastructure
{
    public class UnitOfWork(IDbContext db) : IUnitOfWork
    {
        public async Task BeginTransactionAsync()
            => await db.BeginTransactionAsync();

        public async Task CommitAsync()
            => await db.CommitAsync();

        public async Task RollBackAsync()
            => await db.RollBackAsync();
    }
}
