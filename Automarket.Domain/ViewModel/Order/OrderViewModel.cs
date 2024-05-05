using Automarket.Domain.Enum;
using Automarket.Domain.ViewModel.Car;
using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.ViewModel.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public decimal CarPrice { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [StringLength(50, MinimumLength = 6,  ErrorMessage = "Довжина {0} має бути від {2} до {1} символів")]
        public string PIB { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [StringLength(13, ErrorMessage = "Довжина {0} має бути {1} символів")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Невірний формат тел. номеру. Використовуйте формат: '+380xxxxxxxxx'")]
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
		public OrderViewModel()
        {

        }

		public OrderViewModel(Entity.Order order)
		{
            OrderId = order.Id;
            PIB = order.PIB;
            Phone = order.Phone;
            Status = order.Status;
            Username = order.User.Name;
            CarId = order.Car.Id;
            CarName = order.Car.Name;
            CarPrice = order.Car.Price;
            Date = order.Date;
		}

		public OrderViewModel(CarViewModel carVM)
		{
            CarId = carVM.Id;
            CarName = carVM.Name;
            CarModel = carVM.Model;
            CarPrice = carVM.Price;
		}
	}
}