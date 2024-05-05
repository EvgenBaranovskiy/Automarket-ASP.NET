
namespace Automarket.DAL.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext dbContext;
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
