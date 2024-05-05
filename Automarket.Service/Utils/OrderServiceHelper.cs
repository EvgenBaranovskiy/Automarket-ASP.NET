using Automarket.Domain.Entity;
using Automarket.Domain.ViewModel.Car;
using Automarket.Domain.ViewModel.Order;

namespace Automarket.Service.Utils
{
    static public class OrderServiceHelper
    {
        static public int GetTotalPageNumber(List<Order> orders, int amountPerPage)
        {
            return (int)Math.Ceiling((double)orders.Count / amountPerPage);
        }
        static public bool IsLastPage(List<Order> orders, int amountPerPage, int currentPage)
        {
            return GetTotalPageNumber(orders, amountPerPage) == currentPage;
        }

	}
}
