using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Users;
using Domain.ValueObjects.Users;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository(IDbContext db) : IUserRepository
    {
        public Task<UserId> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(UserId id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetAsync(UserId id)
        {
            throw new NotImplementedException();
        }

        public Task<UserId?> GetIdByEmailAsync(UserEmail email)
        {
            throw new NotImplementedException();
        }

        public Task<UserId?> GetIdByPhoneNumberAsync(UserPhoneNumber phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<UserId?> GetIdByUsernameAsync(Username username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
