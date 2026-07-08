using Domain.Models.PopcornShiftProducts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IPopcornShiftProductRepository
    {
        Task<bool> AddAsync(PopcornShiftProduct popcornShiftProduct);
        Task<bool> DeleteAsync(PopcornShiftProduct popcornShiftProduct);
    }
}
