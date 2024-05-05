
using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext){}

        public async Task<int> CreateAsync(User entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            dbContext.Users.Remove(entity);
            return await dbContext.SaveChangesAsync() == 1;
        }

        public async Task<User> GetAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync((u)=> u.Id == id);
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            var result = await dbContext.Users.FirstOrDefaultAsync((u) => u.Name == username);
            return result;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var result = await dbContext.Users.FirstOrDefaultAsync((u) => u.Name == username);
            return result == null;
        }

        public async Task<List<User>> SelectAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
