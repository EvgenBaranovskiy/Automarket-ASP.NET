using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Repositories
{
	public class OrderRepository : BaseRepository, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<int> CreateAsync(Order entity)
		{
			dbContext.Orders.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<List<Order>> GetOrdersByUserId(int UserId)
		{
			return await dbContext
								.Orders
								.Include(o => o.Car)
								.Include(o => o.User)
								.Where(o => o.User.Id == UserId)
								.ToListAsync();
		}

		public async Task<bool> DeleteAsync(Order entity)
		{
			dbContext.Orders.Remove(entity);
			await dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<Order> GetAsync(int id)
		{
			return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
		}

		public async Task<List<Order>> SelectAsync()
		{
			return await dbContext.Orders
						.Include(o => o.Car)
						.Include(o => o.User)
						.ToListAsync();
		}

		public async Task<bool> UpdateAsync(Order entity)
		{
			dbContext.Update(entity);
			await dbContext.SaveChangesAsync();
			return true;
		}
	}
}