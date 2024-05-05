using Automarket.Domain.Entity;
namespace Automarket.DAL.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserId(int UserId);
	}
}