using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<int> CreateAsync(Car entity)
        {
            dbContext.Cars.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Car entity)
        {
            dbContext.Cars.Remove(entity);
            return await dbContext.SaveChangesAsync() == 1;
        }

        public async Task<Car> GetAsync(int id)
        {
			return await dbContext.Cars
						    .Include(car => car.Images) 
						    .FirstOrDefaultAsync(car => car.Id == id);
		}

        public async Task<Car> GetByNameAsync(string name)
        {
            return await dbContext.Cars.FirstOrDefaultAsync(car => car.Name.Equals(name));
        }

        public async Task<List<Car>> SelectAsync()
        {
            return await dbContext.Cars.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Car entity)
        {
            await dbContext.CarImages.Where(car => car.Id.Equals(entity.Id)).ExecuteDeleteAsync();
            dbContext.Cars.Update(entity);
            return await dbContext.SaveChangesAsync() == 1;
        }
    }
}