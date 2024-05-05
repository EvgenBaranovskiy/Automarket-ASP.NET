using Automarket.Domain.Enum;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModel.Order;

namespace Automarket.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<bool>> CreateOrder(OrderViewModel orderVM);
        Task<IBaseResponse<PageOrdersViewModel>> GetOrders(int page, int pageSize, string username);
        Task<IBaseResponse<PageOrdersViewModel>> GetOrders(int page, int pageSize);
        Task<IBaseResponse<bool>> DeleteOrder(int orderId);
        Task<IBaseResponse<bool>> UpdateOrder(int orderId, OrderStatus newStatus);
	}
}
