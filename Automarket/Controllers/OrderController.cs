using Automarket.Domain.Enum;
using Automarket.Domain.ViewModel.Order;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class OrderController : Controller
    {
        ICarService _carService;
        IOrderService _orderService;
        public OrderController(ICarService carService, IOrderService orderService) 
        { 
            _carService = carService;
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> OrderCarAsync(int id)
        {
            var response = await _carService.GetCarById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
               return View(new OrderViewModel(response.Data));
			}
			return View("~/Views/Shared/Error.cshtml");
		}

        [HttpGet]
        [Authorize(Roles = "Buyer, Admin")]
        public async Task<IActionResult> GetOrders(int page = 1, int pageSize = 3)
        {
            if (User.IsInRole("Buyer"))
            {
                var response = await _orderService.GetOrders(page, pageSize, User.Identity.Name);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data);
                }
            }

            if (User.IsInRole("Admin"))
            {
                var response = await _orderService.GetOrders(page, pageSize);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data);
                }
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
		[Authorize(Roles = "Buyer")]
		public async Task<IActionResult> OrderCarAsync(OrderViewModel orderViewModel)
		{
            if (!User.Identity.Name.Equals(orderViewModel.Username))
            {
                ModelState.AddModelError("Username not matched", "Спроба створити замовлення для іншого користувача!");
            }

            //Перевірка на валідність даних
            if (ModelState.IsValid)
            {
                var response = await _orderService.CreateOrder(orderViewModel);
                switch (response.StatusCode)
                {
                    case Domain.Enum.StatusCode.OK:
						return RedirectToAction("GetOrders", "Order");
					case Domain.Enum.StatusCode.InternalServerError:
                        ModelState.AddModelError("InternalServerError", response.Description);
                        break;
                    default:
                        ModelState.AddModelError(response.StatusCode.ToString(), response.Description);
                        break;
                }
            }
            return View(orderViewModel);
        }

		[HttpPost]
		[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int orderId, int page)
        {
            var response = await _orderService.DeleteOrder(orderId);
			
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetOrders", new { page = page });
			}

			return View("~/Views/Shared/Error.cshtml");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus newStatus, int page)
		{
            var response = await _orderService.UpdateOrder(orderId, newStatus);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
				return RedirectToAction("GetOrders", new { page = page });
			}

			return View("~/Views/Shared/Error.cshtml");
		}

		public IActionResult Index()
        {
            return View();
        }
    }
}