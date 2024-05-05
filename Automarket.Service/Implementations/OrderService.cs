using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModel.Order;
using Automarket.Service.Interfaces;
using Automarket.Service.Utils;

namespace Automarket.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
		private readonly ICarRepository _carRepository;
		public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, ICarRepository carRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _carRepository = carRepository;

		}
        public async Task<IBaseResponse<bool>> DeleteOrder(int orderId)
        {
            try
            {
                var orderToDelete = await _orderRepository.GetAsync(orderId);
				var result = await _orderRepository.DeleteAsync(orderToDelete);

				return new BaseResponse<bool>
                {
                    Data = result,
                    Description = "Замовлення видаленно!",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = $"[DeleteOrder] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> CreateOrder(OrderViewModel orderVM)
        {
            try
            {
                Order newOrder = new Order();
                newOrder.PIB = orderVM.PIB;
                newOrder.User = await _userRepository.GetUserByNameAsync(orderVM.Username);
                newOrder.Car = await _carRepository.GetAsync(orderVM.CarId);
                newOrder.Phone = orderVM.Phone;
                newOrder.Date = DateTime.Now;
                newOrder.Status = OrderStatus.InProcessing;

                var result = await _orderRepository.CreateAsync(newOrder) > 0;

                return new BaseResponse<bool>
                {
                    Data = result,
                    Description = "Замовлення створено!",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = $"[CreateOrder] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<PageOrdersViewModel>> GetOrders(int pageNumber, int pageSize, string username)
        {
            try
            {
                User user = await _userRepository.GetUserByNameAsync(username);
                List<Order> orders = await _orderRepository.GetOrdersByUserId(user.Id);
                orders.Sort((o1, o2) => { return o2.Date.CompareTo(o1.Date); });
                int totalCountPages = OrderServiceHelper.GetTotalPageNumber(orders, pageSize);

                if (pageNumber <= 0 || pageNumber > totalCountPages)
                {
                    return new BaseResponse<PageOrdersViewModel>
                    {
                        Data = null,
                        Description = "Вказанна сторінка не існує!",
                        StatusCode = StatusCode.WrongPageNumber
                    };
                }
                else
                {
                    List<Order> ordersForCurrentPage = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    List<OrderViewModel> ordersVM = new List<OrderViewModel>();
                    
                    foreach(var order in ordersForCurrentPage) {
						ordersVM.Add(new OrderViewModel(order));
					}

					return new BaseResponse<PageOrdersViewModel>
					{
						Data = new PageOrdersViewModel(pageNumber, totalCountPages, ordersVM),
						Description = "Сторінку з замовленнями утворенно!",
						StatusCode = StatusCode.OK
					};
				}
            }
            catch (Exception ex)
            {
                return new BaseResponse<PageOrdersViewModel>
                {
                    Data = null,
                    Description = $"[GetOrders] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PageOrdersViewModel>> GetOrders(int pageNumber, int pageSize)
        {
            try
            {
                List<Order> orders = await _orderRepository.SelectAsync();
                orders.Sort((o1, o2) => { return o2.Date.CompareTo(o1.Date); });
                int totalCountPages = OrderServiceHelper.GetTotalPageNumber(orders, pageSize);

                if (pageNumber <= 0 || pageNumber > totalCountPages)
                {
                    return new BaseResponse<PageOrdersViewModel>
                    {
                        Data = null,
                        Description = "Вказанна сторінка не існує!",
                        StatusCode = StatusCode.WrongPageNumber
                    };
                }
                else
                {
                    List<Order> ordersForCurrentPage = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    List<OrderViewModel> ordersVM = new List<OrderViewModel>();

                    foreach (var order in ordersForCurrentPage)
                    {
                        ordersVM.Add(new OrderViewModel(order));
                    }

                    return new BaseResponse<PageOrdersViewModel>
                    {
                        Data = new PageOrdersViewModel(pageNumber, totalCountPages, ordersVM),
                        Description = "Сторінку з замовленнями утворенно!",
                        StatusCode = StatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<PageOrdersViewModel>
                {
                    Data = null,
                    Description = $"[GetOrders] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		public async Task<IBaseResponse<bool>> UpdateOrder(int orderId, OrderStatus newStatus)
		{
			try
			{
                var orderToUpdate = await _orderRepository.GetAsync(orderId);
                orderToUpdate.Status = newStatus;
                var result = await _orderRepository.UpdateAsync(orderToUpdate);
				return new BaseResponse<bool>
				{
					Data = result,
					Description = $"Статус замовлення успішно оновленно!",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>
				{
					Data = false,
					Description = $"[UpdateOrder] Exception: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
	}
}
