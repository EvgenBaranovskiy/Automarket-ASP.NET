using Automarket.Domain.Entity;

namespace Automarket.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsUsernameUniqueAsync(string username);
        Task<User> GetUserByNameAsync(string username);
    }
}
